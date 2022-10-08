using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Week15Project.Models;
using Week15Project.Models.ViewModels;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class HomeController : Controller
    {
        private IForumRepository repository;

        public HomeController(IForumRepository forumRepository)
        {
            repository = forumRepository;
        }

        public IActionResult Index()
        {
            try
            {
                // User
                //User user = new User() 
                //{ 
                //    Email = "name@mail.com", 
                //    Username = "TestUser1",
                //    Password = "Password",
                //    Permission = false
                //};
                //repository.AddUser(user);

                // Room
                //Room room = new Room()
                //{
                //    RoomName = "TestRoom"
                //};
                //repository.AddRoom(room);

                // Post
                //Post post = new Post()
                //{
                //    RoomId = 1,
                //    UserId = "41baed07-b628-499d-9e6c-16ba4192a358",
                //    DatePosted = DateTime.Now,
                //    Title = "TEST TEST",
                //    Message = "New Post Message",
                //    Locked = false
                //};
                //repository.AddPost(post);

                // Message
                //Response message = new Response()
                //{
                //    PostId = 4,
                //    UserId = "fb3a711c-ac29-4322-97ff-155e55736444",
                //    DatePosted = DateTime.Now,
                //    Message = "Test Response Message"
                //};
                //repository.AddResponse(message);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(new LoginViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}