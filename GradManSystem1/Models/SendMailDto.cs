namespace GradManSystem1.Models
{
    public class SendMailDto : BaseClass
    {
        //  [Required]
        public string Name { get; set; }
        // [Required]
        public string Email { get; set; }
        // [Required]
        public string Message { get; set; }
    }
}
