using System.ComponentModel.DataAnnotations;
namespace GradManSystem1.Models
{
    public class Student : BaseClass
    {
        [Required(ErrorMessage = "Your First Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Your Last Name is required")]
        public string Surname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }
        
    }

}