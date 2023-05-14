using GradManSystem1.Data;
using GradManSystem1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GradManSystem1.Controllers
{
    public class ProductsController : Controller
    {
        public decimal subtotal { get; set; }
        public List<UserProducts> userproducts=new List<UserProducts>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            subtotal = 0;
            ViewBag.subtotal = subtotal;
            var products=_context.Products.ToList();
            return View(products);
        }
        public IActionResult AddToCart(int id)
        {
            subtotal += _context.Products.Where(x => x.Id == id).Select(x => x.Price).FirstOrDefault();
            ViewBag.subtotal = subtotal;
            int currentUserId = Convert.ToInt32(User.Identity.GetUserId());
            var userproduct = new UserProducts()
            {
                UserId = currentUserId,
                ProductId = id
            };
            userproducts.Add(userproduct);
            return View(subtotal);
        }

        public IActionResult Payment()
        {
            return View();
        }
    }
}
