//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using GradManSystem1.Models;
//using GradManSystem1.Areas.Identity.Pages.Account;
//using GradManSystem1.Data;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.Rendering;
//namespace WebApplication6.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly UserManager<ApplicationUser> UserManager;
//        public RoleManager<IdentityRole> RoleManager;
//        public IEnumerable<IdentityRole> Roles { get; set; }
//        public AdminController(UserManager<ApplicationUser> userManager,
//       RoleManager<IdentityRole> roleManager)
//        {
//            this.UserManager = userManager;
//            this.RoleManager = roleManager;

//        }