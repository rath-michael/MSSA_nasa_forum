using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Week15Project.Models;
using Week15Project.Models.ViewModels;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class PostController : Controller
    {
        private IForumRepository repository;
        private UserManager<User> userManager;
        public PostController(IForumRepository forumRepository, UserManager<User> userManager)
        {
            repository = forumRepository;
            this.userManager = userManager;
        }
        public IActionResult ViewPost(int postId)
        {
            PostViewModel model = new PostViewModel()
            {
                Post = repository.GetPost(postId),
                Responses = repository.GetAllResponses(postId)
            };
            return View(model);
        }

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
                return RedirectToAction("ViewPost", new { postId = newResponse.PostId } );
            }
            catch (Exception ex)
            {
                return RedirectToAction();
            }
        }
    }
}
