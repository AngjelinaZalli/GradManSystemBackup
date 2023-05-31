using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayPal.Api;
using System.Data;
using System.Data.Entity;

namespace GradManSystem1.Controllers
{
    public class ProductsController : Controller
    {
        private static decimal Subtotal = 0;
        private static List<UserProducts> userProducts = new List<UserProducts>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;
        IConfiguration _configuration;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration iconfiguration)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _configuration = iconfiguration;
        }

        [Authorize(Roles = "Student")]
        public IActionResult Index()
        {
            ViewBag.subtotal = Subtotal;
            var products = _context.Products.ToList();
            return View(products);
        }
        [Authorize(Roles = "Student")]
        public IActionResult AddToCart(int id)
        {
            var currentUserId = User.Identity.GetUserId().ToString();
            var alreadyBought = _context.UserProducts.Any(x => x.UserId.ToString() == currentUserId && x.ProductId == id);
            var prod = HttpContext.Session.GetString("userprod");
            var products = _context.Products.ToList();
            if (!string.IsNullOrEmpty(prod))
            {
                var deserializedObj = JsonConvert.DeserializeObject<List<UserProducts>>(prod.ToString());
                if (deserializedObj.Select(x => x.ProductId).Contains(id))
                {
                    TempData["message"] = "This product is already in your Shopping Cart.";
                    return View("Index", products);
                }
            }

            if (alreadyBought)
            {
                TempData["message"] = "You have already paid for this course.";
                return View("Index", products);
            }
            else
            {
                Subtotal += _context.Products.Where(x => x.Id == id).Select(x => x.Price).FirstOrDefault();
                ViewBag.subtotal = Subtotal;
                var userProduct = new UserProducts()
                {
                    UserId = Guid.Parse(currentUserId),
                    ProductId = id
                };
                userProducts.Add(userProduct);
                TempData["userprod"] = JsonConvert.SerializeObject(userProducts);
                HttpContext.Session.SetString("userprod", JsonConvert.SerializeObject(userProducts));
            }

            return View("Index", products);
        }

        [Authorize(Roles = "Student")]
        public IActionResult ViewCart()
        {
            var prod = HttpContext.Session.GetString("userprod");
            if (string.IsNullOrEmpty(prod) || prod.Contains("[]"))
            {
                ViewBag.Message = "No Courses in your Cart.";
                return View(null);
            }

            var deserializedObj = JsonConvert.DeserializeObject<List<UserProducts>>(prod.ToString());
            var userProduct = _context.Products
                .Where(x => deserializedObj.Select(x => x.ProductId).Contains(x.Id))
                .ToList();

            return View(userProduct);
        }

        [Authorize(Roles = "Student")]
        public IActionResult PaidCourses(int id)
        {
            var userProduct = _context.UserProducts
                .Include(x => x.Product)
                .Where(x => x.UserId.ToString() == User.Identity.GetUserId().ToString())
                .Select(x => x.Product)
                .ToList();

            if (userProduct.Count == 0)
            {
                ViewBag.Message = "No Paid Courses available.";
            }

            return View(userProduct.DistinctBy(x => x.Id).ToList());
        }

        [Authorize(Roles = "Student")]
        public IActionResult RemoveFromViewCart(int id)
        {
            var prod = HttpContext.Session.GetString("userprod");
            var deserializedObj = JsonConvert.DeserializeObject<List<UserProducts>>(prod.ToString());
            var itemToRemove = deserializedObj.FirstOrDefault(x => x.ProductId == id);
            if (itemToRemove != null)
            {
                deserializedObj.Remove(itemToRemove);
                HttpContext.Session.SetString("userprod", JsonConvert.SerializeObject(deserializedObj));
            }

            var productPrice = _context.Products.Where(x => x.Id == id).FirstOrDefault().Price;
            //TODO get amount from db 
            Subtotal = Subtotal - productPrice;
            return RedirectToAction("ViewCart");
        }

        [Authorize(Roles = "Student")]
        public ActionResult PaymentWithPaypal(string Cancel = null, string blogId = "", string PayerID = "", string guid = "")
        {

            var ClientID = _configuration.GetValue<string>("PayPal:Key");
            var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
            var mode = _configuration.GetValue<string>("PayPal:mode");
            APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);
            try
            {
                //A resource representing a Payer that funds a payment Method as Paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = PayerID;
                if (string.IsNullOrEmpty(payerId))
                {
                    //This section will be executed first because PayerIdd doesn't exist
                    //it is returned by the create function call of the payment class
                    //Creating a payment
                    //baseURL is the url on which paypal sends back the data
                    string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Products/PaymentWithPayPal?";
                    //Here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution
                    var guidd = Convert.ToString((new Random()).Next(100000));
                    guid = guidd;
                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, blogId);
                    //Get links returned from paypal in responce to Create function call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the paypalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    _httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  

                    var paymentId = _httpContextAccessor.HttpContext.Session.GetString("payment");
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {

                        return View("PaymentFailed");
                    }

                    if (TempData.ContainsKey("userprod"))
                    {
                        var prod = TempData["userprod"];
                        _context.AddRange(userProducts.DistinctBy(x => x.ProductId).Where(x => x.Id == 0));
                        _context.SaveChanges();
                    }
                    var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
                    return View("PaymentSuccess");
                }
             
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {
                return View("PaymentFailed");
            }
            //On successful payment,show success page to user
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl, string blogId)
        {
            //Create itemlist and add item objects to it
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new Item()
            {
                name = "Item Detail",
                currency = "USD",
                price = Subtotal.ToString("F"),
                quantity = "1",
                sku = "asd"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = Subtotal.ToString(),
            };
            var transactionList = new List<Transaction>();
            //Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Guid.NewGuid().ToString(),//Generate an Invoice No
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


    }

}

