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
        public ForumController(IForumRepository _repository, UserManager<User> _userManager)
        {
            repository = _repository;
            userManager = _userManager;
        }

        #region Room
        [AllowAnonymous]
        public IActionResult ViewAllRooms()
        {
            var rooms = repository.GetAllRooms();
            return View(rooms);
        }

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
                repository.AddPost(newPost);
                return RedirectToAction("ViewRoom", new { roomId = newPost.RoomId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult ViewPost(int postId)
        {
            Post post = repository.GetPost(postId);
            if (post != null)
            {
                return View(post);
            }
            return RedirectToRoute("Error", "Home");
        }

        public IActionResult ViewNewestPost()
        {
            Post newestPost = repository.GetNewestPost();
            if (newestPost != null)
            {
                return RedirectToAction("ViewPost", new { postId = newestPost.PostId });
            }
            return RedirectToRoute("Error", "Home");
        }

        public IActionResult ViewMostPopularToday()
        {
            Post popularPost = repository.GetMostPopularPostToday();
            if (popularPost != null)
            {
                return RedirectToAction("ViewPost", new { postId = popularPost.PostId });
            }
            return RedirectToAction("Error", "Home");
        }

        //public IActionResult EditPost(int postId)
        //{
        //    Post post = repository.GetPost(postId);
        //    if (post != null)
        //    {
        //        return View(post);
        //    }
        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult EditPost(Post post)
        {
            repository.EditPost(post);
            return RedirectToAction("ViewPost", new { postId = post.PostId });
        }

        public IActionResult DeletePost(int postId)
        {
            var post = repository.GetPost(postId);
            try
            {
                repository.DeletePost(postId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("ViewRoom", new { roomId = post.RoomId });
        }
        #endregion

        #region Response
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
            }
            catch (Exception ex)
            {
                // LOG ERROR HERE
            }
            return RedirectToAction("ViewPost", new { PostId = newResponse.PostId });
        }

        public IActionResult EditResponse(int responseId)
        {
            Response response = repository.GetResponse(responseId);
            if (response != null)
            {
                return View(response);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult EditResponse(Response response)
        {
            repository.EditResponse(response);
            return RedirectToAction("ViewPost", new { postId = response.PostId });
        }

        public IActionResult DeleteResponse(int responseId)
        {
            Response response = repository.GetResponse(responseId);
            try
            {
                repository.DeleteResponse(responseId);
            }
            catch (Exception ex)
            {
                // LOG ERROR HERE
                throw;
            }
            return RedirectToAction("ViewPost", new { postId = response.PostId });
        }
        #endregion
    }
}
