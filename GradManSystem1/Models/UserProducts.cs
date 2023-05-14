namespace GradManSystem1.Models
{
    public class UserProducts
    { 
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; } 
    }
}
