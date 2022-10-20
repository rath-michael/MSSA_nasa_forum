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

            builder.Entity<Post>(entity =>
            {
                entity.HasOne(r => r.Room)
                    .WithMany(p => p.Posts)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.User)
                    .WithMany(p => p.Posts)
                    .OnDelete(DeleteBehavior.SetNull);

            });

            builder.Entity<Response>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Responses)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Responses)
                    .OnDelete(DeleteBehavior.SetNull);
            });




            //=======================

            builder.Entity<Room>().HasData(
                new Room() { RoomId = 1, RoomName = "General Discussion", RoomDescription = "General space exploration discussion. All topics welcome here." },
                new Room() { RoomId = 2, RoomName = "Advanced Topics", RoomDescription = "Topics about future space flights and how they might be done." },
                new Room() { RoomId = 3, RoomName = "International Space Station", RoomDescription = "Discussion about the International Space Station." },
                new Room() { RoomId = 4, RoomName = "Q&A Section", RoomDescription = "Ask a specific question. Get a specific answer." },
                new Room() { RoomId = 5, RoomName = "SpaceX", RoomDescription = "Past missions. Current missions. Future missions. All here." },
                new Room() { RoomId = 6, RoomName = "Images & Videos", RoomDescription = "A place to share images and videos." },
                new Room() { RoomId = 7, RoomName = "Events", RoomDescription = "A place to discuss previous & upcoming events as seen in the 'Events' Page"}
                );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "32" },
                new IdentityRole() { Id = "2", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "11" }
                );

            builder.Entity<User>().HasData(
                new User() { Id = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", FirstName = "Michael", LastName = "Rath", UserName = "Admin", DateCreated = new DateTime(2022, 9, 20), Color = "#ffc107", NormalizedUserName = "ADMIN", Email = "name@mail.com", NormalizedEmail = "NAME@MAIL.COM", EmailConfirmed = false, PasswordHash = "AQAAAAEAACcQAAAAEBd61nKt0F2OlXFPD/cNJRdZvHsA6Ly5vlpUrc/FbmtVwj8+dl3UZ1Fl/HTyi2xwzA==", SecurityStamp = "WBCQHR57FRNZYS3GNHB3GR27NKLKLGSW", ConcurrencyStamp = "402cf21a-dd0c-4d39-b81c-b35b606364a7", PhoneNumber = "4254429927", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0 },
                new User() { Id = "fb3a711c-ac29-4322-97ff-155e55736444", FirstName = "Michael", LastName = "Rath", UserName = "User", DateCreated = new DateTime(2022, 9, 21), Color = "#17a2b8", NormalizedUserName = "USER", Email = "name@mail.com", NormalizedEmail = "NAME@MAIL.COM", EmailConfirmed = false, PasswordHash = "AQAAAAEAACcQAAAAEF5kLocdLNHhMzrw3OMuwuaT8auZwWjNdHlcHqRhlihIUGY59rSE2nBvS3NwxxREOg==", SecurityStamp = "GO4ER5WN6ZAGYTFCDJBM6UOPAOLIHPXF", ConcurrencyStamp = "6ca3a9a9-6046-43fe-bacd-c7c403c1f1ab", PhoneNumber = "4254429927", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0 }
                );

            builder.Entity<Post>().HasData(
                new Post() { PostId = 7, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", RoomId = 1, DatePosted = DateTime.Now, Title = "Is spaceflight important? Why?", Message = "My daughter was given an essay question today for her 3rd grade science class after reading about the U.S. space program. She had only three lines to respond... The essay question is: 'Is spaceflight important ? Why ?' She gave a pretty good answer already, so I won't ask forum members to do her homework for her, but I thought I'd put the same question out to NSF brethren.  Perhaps one of us has a 'Yes, Virginia there is a Santa Claus...' response that will reverberate beyond our forum. So, IS spaceflight important? Why?", UserImage = null, WebURL = "http://www.google.com" },
                new Post() { PostId = 8, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", RoomId = 2, DatePosted = DateTime.Now, Title = "Solar Power Satellites", Message = "If Japan really decides to build one or more 10, 000 - ton solar power farms, that would provide business for lots of launches. 'http://spectrum.ieee.org/green-tech/solar/how-japan-plans-to-build-an-orbital-solar-farm The IEEE article includes lots of reasons why this may never get off the ground, of course, but it's interesting to think about what sort of economies of scale might be possible given projects like this. And what sort of projects might be possible given further reductions in price.", UserImage = null},
                new Post() { PostId = 9, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", RoomId = 3, DatePosted = DateTime.Now, Title = "Biden-Harris Administration Extends Space Station Operations Through 2030", Message = "Biden - Harris Administration Extends Space Station Operations Through 2030: https://blogs.nasa.gov/spacestation/2021/12/31/biden-harris-administration-extends-space-station-operations-through-2030/ ", UserImage = null },
                new Post() { PostId = 10, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", RoomId = 4, DatePosted = DateTime.Now, Title = "When will Orion first dock on space?", Message = "It seems like there will be no in-space test of Orion's ability to rendezvous and dock prior to the Artemis III deocking in NRHO. Is this correct? Is this reasonable?", UserImage = null },
                new Post() { PostId = 11, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", RoomId = 5, DatePosted = DateTime.Now, Title = "Improved F9 first stage using StarShip heat shield tiles", Message = "Right now, the F9 uses about 20 tonnes of fuel slow itself down (during the entry burn) -roughly 3 engines x 20 seconds x 330 kg/sec/engine.  Likely this is done to keep the heating down to an acceptable level for the aluminum structure. \nSo if we add a heat shield to the aluminum, assuming the engine section is already tough enough, perhaps F9 could re-enter using aerodynamic deceleration only.How much would a heat shield mass?  The stage is about 41 meters long and 3.7 meters in diameter, hence about 500 m^2.  Assuming a shuttle low-density tile mass of 144 kg/m^2, and a thickness of 4 cm (it's not much heating compared to shuttle entry) then the mass of the heat shield is 3 tonnes - clearly a win. A quick calculation, making the usual assumptions [ 311 First stage ISP, 436t First stage fuel, 27t First stage structure. (30t with heat shield),  26t Landing reserve(6t with heat shield),  348 Second stage ISP, 4t Mass of fairing, 107t Second stage fuel, 5.5t Second stage structure, 16t Payload says this would increase LEO payload by 2 tonnes.The first stage can provide about 250 m/s more, second stage 250 m/s less due to increased payload. I doubt SpaceX would do this (it's too close to F9 end of life, unless StarShip flops) but it shows that F9 could definitely be improved on.", UserImage = null }
                );

            builder.Entity<Response>().HasData(
                new Response() { ResponseId = 5, PostId = 7, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "When I was a kid and asked my parents why (just pick the topic), I would sometimes hear 'Because I said so.' Does that work?" },
                new Response() { ResponseId = 6, PostId = 7, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Spaceflight is important because in “seeking the truth” it will lead to the “answers of the big three questions”.\nAre we all that there “is, was and will ever become?”" },
                new Response() { ResponseId = 7, PostId = 7, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "IS spaceflight important? Why?\n1) Yes.\nWhy?\n2) It's how things get to orbit.\n3) Are we there yet!  ;D\n\nOK, so some day we can get off this rock and explore new worlds and settle them if we wish to.\nTo hopefully better understand our universe we exist in.\nBecause I want space flight and we can." },
                new Response() { ResponseId = 8, PostId = 8, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "They have a long way to go to compete with terrestrial solar power but there some technologies in pipeline to make it more viable.\nSpaceX BFR if it us fully reusable.\nTethers Unlimited SpiderFab.\nIncreasing Watts/ kg of solar panels.\nJapan has talked about beaming power from moon to earth but actually sending power to lunar base from SSP satellite or even earth may be ideal for supplying power 24/7." },
                new Response() { ResponseId = 9, PostId = 8, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Unlike, Musk I think the conversion rates can be made efficient enough for this to be physically practical.  What makes it difficult is the initial cost in infrastructure which makes it non-competitive over other forms of power though. I think fusion research has a better chance in the short term of creating results than all the things that would have to come together to make space based solar power economical.\n\nOn a positive note you might be able to increase the efficiency of your microwave rectenna by using photonic crystals to tune the incoming microwaves for better absorption. You could also put your rectenna on a ground tethered high altitude dirigible to lower losses and prevent communication interference posed by beaming through the lower atmosphere." },
                new Response() { ResponseId = 10, PostId = 8, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Beaming power to earth is a pretty non starter (efficency of energy transfer) but using large solar plants to power antimater factories seems plausable .\n\nPlus this would usher in the antimater era and give humanity the stars ." },
                new Response() { ResponseId = 11, PostId = 9, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Not surprisingly, ESA is on board with the extension:\nESA is proud to be a partner of @NASA, @roscosmos, @JAXA_en, & @csa_asc on board the ISS. Big projects require common goals & the accumulated excellence of all." },
                new Response() { ResponseId = 12, PostId = 9, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "So Russia is onboard too?" },
                new Response() { ResponseId = 13, PostId = 9, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Gives us just enough time for Axiom Station to be fully self-sufficient.  I am very optimistic about Axiom if for no other reason on the basis of who is running it. The only private company in the world who has a staff who has actually been responsible for successfully building an operational space station.\nOne thing I have been curious about.  At the point that Axiom is at the self-sufficient stage, would the the US part of the station be able to operate if the Russian segment was disconnected?" },
                new Response() { ResponseId = 14, PostId = 9, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Robyn Gatens' presentation on the ISS extension:\nhttps://www.nasa.gov/sites/default/files/atoms/files/nacjan2022_iss_final.pdf" },
                new Response() { ResponseId = 15, PostId = 10, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Correct, and yes. The Orion docking system design is the same as is used on Starliner, so it has already been tested in space, and will be used for docking with the ISS a few times before Artemis III flies.\nAlso, Artemis II will conduct proximity operations with the upper stage after separation after the TLI burn." },
                new Response() { ResponseId = 16, PostId = 10, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "No, it is not a good operations decision.\nIt's also not the only thing that will be untested in flight either;  The life support systems will not be flight tested on Artemis 1.    The Euro Service module does not have all the life support consumable tanks installed on Artemis 1\nThere will be no rendezvous and docking test between Orion and the HLS lander prior to the moon landing attempt.\nI find the argument of commonality with Starliner unconvincing, as I have doubts that the software, sensors and computing are actually identical with Orion. Also, Starliner hasnt' and won't dock with HLS." },
                new Response() { ResponseId = 17, PostId = 10, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "Artemis II will test pretty much everything except the actual docking, all in an Earth orbit before departing for the Moon. That's why I asked about the docking. It would be nice if NASA would pay SpaceX to make an HLS available in Earth orbit during Artemis II. If they somehow magically integrate this with the HLS demo flight, the incremental cost would presumably be low.\nCommenters here have also mentioned that Starliner's dock is active-only but that Orion's will be active/passive. If so, then they may be similar but they are not identical." },
                new Response() { ResponseId = 18, PostId = 11, UserId = "158c4445-8ce9-49ea-9fcb-5a9a2e76f63a", DatePosted = DateTime.Now, Message = "A secondary effect of the entry burn (or perhaps the primary effect) is to push the shock wave down away from the vehicle. Hypersonic shock waves cause extreme heating, as X-15A-2 demonstrated." }
                );
        }
    }
}