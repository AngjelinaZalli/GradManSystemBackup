using System.ComponentModel.DataAnnotations;

namespace GradManSystem1.Models
{
    public class UserProducts
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        //public virtual User User { get; set; }
        public virtual Products Product { get; set; }
    }
}
