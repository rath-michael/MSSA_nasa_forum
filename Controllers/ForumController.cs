using Microsoft.AspNetCore.Mvc;
using Week15Project.Services;
using Week15Project.Models.ViewModels;
using Week15Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// Action to return all rooms currently available from database
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ViewAllRooms()
        {
            var rooms = repository.GetAllRooms();
            return View(rooms);
        }
        /// <summary>
        /// Action to return room with specified roomId from database
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ViewRoom(int roomId)
        {
            Room room = repository.GetRoom(roomId);
            if (room == null)
            {
                return RedirectToRoute("Error", "Home");
            }
            else
            {
                foreach (var item in room.Posts)
                {
                    item.User = repository.GetUser(item.UserId);
                }
                return View(room);
            }
        }
        #endregion

        #region Post
        /// <summary>
        /// Action takes roomId, creates new post that has this roomId associated with it,
        /// sends new post to view. This view gives the user the option to add title and message
        /// parameters, and then submitting the post
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        public IActionResult NewPost(int roomID)
        {
            Post newPost = new Post()
            {
                RoomId = roomID,
            };
            return View(newPost);
        }
        /// <summary>
        /// Action that takes new post object passed to NewPost.cshtml with title and message properties
        /// added, then adds correct UserId, date and time of post, and whether post is locked or not,
        /// then submits this new post to the ForumRepository for addition to the database.
        /// </summary>
        /// <param name="newPost"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IActionResult NewPost(Post newPost)
        {
            try
            {
                newPost.UserId = userManager.GetUserId(User);
                newPost.DatePosted = DateTime.Now;
                repository.AddPost(newPost);
                return RedirectToAction("ViewRoom", new { roomId = newPost.RoomId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }
        /// <summary>
        /// Action that returns a PostViewModel to a view, so that the post can be displayed along
        /// with all responses to that post.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IActionResult ViewPost(int postId)
        {
            Post post = repository.GetPost(postId);
            if (post != null)
            {
                return View(post);
            }
            return RedirectToRoute("Error", "Home");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewNewestPost()
        {
            Post newestPost = repository.GetNewestPostId();
            if (newestPost != null)
            {
                return RedirectToAction("ViewPost", new { postId = newestPost.PostId });
            }
            return RedirectToRoute("Error", "Home");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewMostPopularToday()
        {
            Post popularPost = repository.GetMostPopularPostId();
            if (popularPost != null)
            {
                return RedirectToAction("ViewPost", new { postId = popularPost.PostId });
            }
            return RedirectToRoute("Error", "Home");
        }
        #endregion

        #region Response
        /// <summary>
        /// Action takes postID, creates new response that has this postID associated with it,
        /// sends new response to view. This view gives the user the option to add message
        /// parameter, and then submitting the response
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        public IActionResult NewResponse(int postID)
        {
            Response response = new Response()
            {
                PostId = postID
            };
            return View(response);
        }
        /// <summary>
        /// Action that takes new response object passed to NewResponse.cshtml with message property
        /// added, then adds correct UserId, date and time of post, then submits this new post to 
        /// the ForumRepository for addition to the database.
        /// </summary>
        /// <param name="newResponse"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NewResponse(Response newResponse)
        {
            try
            {
                newResponse.UserId = userManager.GetUserId(User);
                newResponse.DatePosted = DateTime.Now;
                repository.AddResponse(newResponse);
            }
            catch (Exception ex)
            {
                // LOG ERROR HERE
            }
            return RedirectToAction("ViewPost", new { PostId = newResponse.PostId });
        }
        #endregion
    }
}
