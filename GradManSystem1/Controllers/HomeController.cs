using DNTCaptcha.Core;
using GradManSystem1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace GradManSystem1.Controllers
{
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SendMailDto sendMailDto;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }



        //[HttpPost]
        //public async Task<IActionResult> SignIn(ViewModel.SignInViewModel SignInViewModel)
        //{
        //    if (!ModelState.IsValid) return View(SignInViewModel);

        //    return View(SignInViewModel);
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact([Bind("Name,Email,Message")] SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                MailMessage mail = new MailMessage();

                //admin@gmail.com
                mail.From = new MailAddress("seele.verlorene0@gmail.com");
                mail.To.Add("zalli.angjelina@gmail.com");

                // mail.Email = SendMailDto.Email;
                // mail.CC.Add("");
                // mail.Bcc.Add("");

                mail.IsBodyHtml = true;
                string content = "Name : " + sendMailDto.Name;
                content += "<br/> Message: " + sendMailDto.Message;

                mail.Body = content;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 25);

                NetworkCredential networkCredential = new NetworkCredential("seele.verlorene0@gmail.com", "Lostsoul2002");
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = 25;
                smtpClient.EnableSsl = true;
                smtpClient.Host= "relay-hosting.secureserver.net";
                smtpClient.Send(mail);
                smtpClient.Timeout = 1200;
                ViewBag.Message = "Mail Send";

                ModelState.Clear();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}