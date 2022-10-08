using Microsoft.AspNetCore.Identity;

namespace Week15Project.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}
