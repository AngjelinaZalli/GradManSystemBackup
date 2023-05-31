using GradManSystem1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradManSystem1.Controllers
{
    public class RoleController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleController(SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit()
        {
            return View();
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = "Admin")]
        //Retrieves a list of users
        public async Task<IActionResult> Users(string searchString)
        {
            var users = from c in _context.Users select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(c => c.UserName.StartsWith(searchString));
            }
            List < Microsoft.AspNetCore.Identity.IdentityUser > user = GetListUsers();

            return View(await users.ToListAsync());
        }

        private List<Microsoft.AspNetCore.Identity.IdentityUser> GetListUsers()
        {
            var users = _signInManager.UserManager.Users;
            var user = new List<Microsoft.AspNetCore.Identity.IdentityUser>();
            foreach (var item in users)
            {
                var userData = new Microsoft.AspNetCore.Identity.IdentityUser
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed
                };
                user.Add(userData);
            }

            return user;
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var userRoles = _roleManager.Roles.ToList();
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid user");
                return View();
            }
            var selectedUserRole = await _userManager.GetRolesAsync(user);
            var userList = new List<UserListDTO>();
            foreach (var item in userRoles)
            {
                var userRole = new UserListDTO
                {
                    IdentityRole = new IdentityRole
                    {
                        Id = item.Id,
                        NormalizedName = item.NormalizedName,
                        Name = item.Name
                    },
                    Selected = selectedUserRole.Contains(item.Name) ? true : false
                };
                userList.Add(userRole);
            }
            ViewBag.Id = id;
            return View(userList);
        }
        public class UserListDTO
        {
            public IdentityRole? IdentityRole { get; set; }
            public bool Selected { get; set; }
        }
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> SaveUser(string id)
        {
            var roleId = Request.Query["foo"].ToString();

            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid edit user attempt");
                return View();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            await _userManager.AddToRoleAsync(user, roleId);
            List<Microsoft.AspNetCore.Identity.IdentityUser> users = GetListUsers();
            return View("Users", users);
        }

        [Authorize(Policy = "Student")]
        public IActionResult GetGrades()
        {
            return View();
        }
    }
}
