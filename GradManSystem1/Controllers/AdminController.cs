using Microsoft.AspNetCore.Mvc;

namespace GradManSystem1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
