using System.ComponentModel.DataAnnotations;
namespace GradManSystem1.Models
{
    public class Grade : BaseClass
    {
        internal string UserId;

        [Required]
        public int GradeMark { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CoursesId { get; set; }
        [Required]
        public int ProffesorId { get; set; }
        //[Required]
        //public Student Student { get; set; }
        //[Required]
        //public Courses Courses { get; set; }
        //[Required]
        //public Proffesor Proffesor { get; set; }

    }
}
