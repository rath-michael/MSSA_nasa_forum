using Microsoft.AspNetCore.Mvc;
using Week15Project.Services;
using Week15Project.Models.ViewModels;
using Week15Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Week15Project.Controllers
{
    public class ForumController : Controller
    {
        private IForumRepository repository;
        private UserManager<User> userManager;
        public ForumController(IForumRepository forumRepository, UserManager<User> userManager)
        {
            repository = forumRepository;
            this.userManager = userManager;
        }

        #region Room
        public IActionResult ViewAllRooms()
        {
            var rooms = repository.GetAllRooms();
            return View(rooms);
        }

        public IActionResult ViewRoom(int roomId)
        {
            Console.WriteLine();
            RoomViewModel viewModel = new RoomViewModel()
            {
                Room = repository.GetRoom(roomId),
                Posts = repository.GetAllPosts(roomId)
            };
            return View(viewModel);
        }
        #endregion

        #region Post
        public IActionResult NewPost(int roomID)
        {
            Post newPost = new Post()
            {
                RoomId = roomID,
            };
            return View(newPost);
        }
        [HttpPost]
        public IActionResult NewPost(Post newPost)
        {
            try
            {
                newPost.UserId = userManager.GetUserId(User);
                newPost.DatePosted = DateTime.Now;
                newPost.Locked = false;
                repository.AddPost(newPost);
                return RedirectToAction("ViewRoom", new { roomId = newPost.RoomId } );
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        // Action that gets all data necessary to display an individual post
        public IActionResult ViewPost(int postId)
        {
            PostViewModel model = new PostViewModel()
            {
                Post = repository.GetPost(postId),
                Responses = repository.GetAllResponses(postId)
            };
            return View(model);
        }
        // new response
        public IActionResult NewResponse(int postID)
        {
            Response response = new Response()
            {
                PostId = postID
            };
            return View(response);
        }

        [HttpPost]
        public IActionResult NewResponse(Response newResponse)
        {
            try
            {
                newResponse.UserId = userManager.GetUserId(User);
                newResponse.DatePosted = DateTime.Now;
                repository.AddResponse(newResponse);
                return RedirectToAction("ViewPost", new { postId = newResponse.PostId });
            }
            catch (Exception ex)
            {
                return RedirectToAction();
            }
        }
        #endregion
    }
}
