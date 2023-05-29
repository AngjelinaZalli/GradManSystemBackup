using Microsoft.AspNetCore.Identity;

namespace GradManSystem1.Models
{
    public class AddUserViewModel
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        //public DateTime BirthDate { get; set; }

        //public string Qyteti { get; set; }

        public RoleManager<IdentityRole> RoleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
