using GradManSystem1.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GradManSystem1.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult SignIn()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel SignInViewModel)
        {
            if (!ModelState.IsValid) return View(SignInViewModel);

            return View(SignInViewModel);
        }
    }
}
