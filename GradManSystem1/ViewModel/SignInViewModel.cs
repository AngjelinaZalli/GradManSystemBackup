using System.ComponentModel.DataAnnotations;

namespace GradManSystem1.ViewModel
{
	public class SignInViewModel
	{
        

        [Display(Name = "Email Address")]

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
	}
}
