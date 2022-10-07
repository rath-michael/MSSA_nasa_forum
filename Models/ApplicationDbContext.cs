using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Room>().HasData(
                new Room() { RoomId = 1, RoomName = "General Discussion", RoomDescription = "General space exploration discussion. All topics welcome here." },
                new Room() { RoomId = 2, RoomName = "Advanced Topics", RoomDescription = "Topics about future space flights and how they might be done." },
                new Room() { RoomId = 3, RoomName = "International Space Station", RoomDescription = "Discussion about the International Space Station." },
                new Room() { RoomId = 4, RoomName = "Q&A Section", RoomDescription = "Ask a specific question. Get a specific answer." },
                new Room() { RoomId = 5, RoomName = "SpaceX", RoomDescription = "Past missions. Current missions. Future missions. All here." },
                new Room() { RoomId = 6, RoomName = "Images & Videos", RoomDescription = "A place to share images and videos." }
                );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "32" },
                new IdentityRole() { Id = "2", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "11" }
                );
        }
    }
}
