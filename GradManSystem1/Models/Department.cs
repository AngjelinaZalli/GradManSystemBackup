using System.ComponentModel.DataAnnotations;
namespace GradManSystem1.Models
{
    public class Department : BaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int NumberOfCourses { get; set; }

        public List<Courses> Courses { get; set; }
            
        //public List<Proffesor> Proffesors { get; set; }

        //public string Image { get; set; } = String.Empty;
        //[NotMapped]
        //[DataType(DataType.Upload)]
        //[Display(Name = "Upload image")]
        //[Required]
        //public IFormFile? ImageFile { get; set; }
    }
}
