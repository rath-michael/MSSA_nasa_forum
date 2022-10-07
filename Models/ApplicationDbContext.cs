using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Week15Project.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Response> Responses { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
    }
}
