using Microsoft.AspNetCore.Identity;

namespace Week15Project.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
