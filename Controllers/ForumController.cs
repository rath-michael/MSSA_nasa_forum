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
        [Route("All")]
        public IActionResult ViewAllRooms()
        {
            var rooms = repository.GetAllRooms();
            if (rooms == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(rooms);
        }

        [AllowAnonymous]
        [Route("Room/{roomId}")]
        public IActionResult ViewRoom(int roomId)
        {
            Room room = repository.GetRoom(roomId);
            if (room == null)
            {
                return RedirectToAction("Error", "Home");
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
        [Route("NewPost")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult NewPost(int roomID)
        {
            if (roomID > 0)
            {
                Post newPost = new Post()
                {
                    RoomId = roomID,
                };
                return View(newPost);
            }
            return RedirectToAction("Error", "Home");
        }

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
                return RedirectToAction("Error", "Home");
            }
        }

        [Route("Post/{postId}")]
        public IActionResult ViewPost(int postId)
        {
            Post post = repository.GetPost(postId);
            if (post != null)
            {
                return View(post);
            }
            return RedirectToAction("Error", "Home");
        }

        [Route("NewestPost")]
        public IActionResult ViewNewestPost()
        {
            try
            {
                Post newestPost = repository.GetNewestPost();
                return RedirectToAction("ViewPost", new { postId = newestPost.PostId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Route("PopularPost")]
        public IActionResult ViewMostPopularToday()
        {
            try
            {
                Post popularPost = repository.GetMostPopularPostToday(); ;
                return RedirectToAction("ViewPost", new { postId = popularPost.PostId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
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
        [Route("EditPost")]
        [HttpPost]
        public IActionResult EditPost(Post post)
        {
            try
            {
                repository.EditPost(post);
                return RedirectToAction("ViewPost", new { postId = post.PostId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        
        public IActionResult DeletePost(int postId)
        {
            try
            {
                var post = repository.GetPost(postId);
                repository.DeletePost(postId);
                return RedirectToAction("ViewRoom", new { roomId = post.RoomId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region Response
        //[Route("NewResponse")]
        //public IActionResult NewResponse(int postID)
        //{
        //    if (postID > 0)
        //    {
        //        Response response = new Response()
        //        {
        //            PostId = postID
        //        };
        //        return View(response);
        //    }
        //    return RedirectToAction("Error", "Home");
        //}

        [HttpPost]
        public IActionResult NewResponse(Response newResponse)
        {
            try
            {
                newResponse.UserId = userManager.GetUserId(User);
                newResponse.DatePosted = DateTime.Now;
                repository.AddResponse(newResponse);
                return RedirectToAction("ViewPost", new { PostId = newResponse.PostId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        //public IActionResult EditResponse(int responseId)
        //{
        //    Response response = repository.GetResponse(responseId);
        //    if (response != null)
        //    {
        //        return View(response);
        //    }
        //    return NotFound();
        //}
        [HttpPost]
        public IActionResult EditResponse(Response response)
        {
            try
            {
                repository.EditResponse(response);
                return RedirectToAction("ViewPost", new { postId = response.PostId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult DeleteResponse(int responseId)
        {
            try
            {
                Response response = repository.GetResponse(responseId);
                repository.DeleteResponse(responseId);
                return RedirectToAction("ViewPost", new { postId = response.PostId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion
    }
}
