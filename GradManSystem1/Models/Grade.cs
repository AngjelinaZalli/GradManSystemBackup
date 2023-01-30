using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradManSystem1.Models
{
    public class Grade : BaseClass
    {
        [Required]
        public int GradeMark { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CoursesId { get; set; }
        [Required]
        public int ProffesorId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Courses Courses { get; set; }
        public virtual  Proffesor Proffesor { get; set; }
    }
}
