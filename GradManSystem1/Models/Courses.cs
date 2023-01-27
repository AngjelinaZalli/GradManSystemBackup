using System.ComponentModel.DataAnnotations;


namespace GradManSystem1.Models
{
    public class Courses : BaseClass
    {
        [Required(ErrorMessage = "Name of the course is required"),MinLength(3)]
        
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }

    }
}
