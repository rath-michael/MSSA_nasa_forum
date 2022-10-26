using Microsoft.EntityFrameworkCore;
using Week15Project.Models;

namespace Week15Project.Services
{
    public class ForumRepository : IForumRepository
    {
        private ApplicationDbContext context;
        public ForumRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region Room CRUD
        // Add Room
        public void AddRoom(Room room)
        {
            if (room != null)
            {
                context.Rooms.Add(room);
                context.SaveChanges();
            }
        }
        // Get Room
        public Room GetRoom(int roomId)
        {
            return context.Rooms
                .Include(p => p.Posts)
                .Where(x => x.RoomId == roomId)
                .Single();
        }
        // Get All Rooms
        public List<Room> GetAllRooms()
        {
            return context.Rooms.Include(p => p.Posts).ToList();
        }
        #endregion

        #region User CRUD
        // Add User
        public void AddUser(User user)
        {
            if (user != null)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        // Get User
        public User GetUser(string userId)
        {
            return context.Users
                .Include(r => r.Responses)
                .Include(p => p.Posts)
                .Where(x => x.Id == userId)
                .Single();
        }
        // Get User by UserName
        public User GetUserByName(string username)
        {
            return context.Users.Where(u => u.UserName == username).FirstOrDefault();
        }
        // Get All Users
        public List<User> GetAllUsers()
        {
            return context.Users
                .Include(p => p.Posts)
                .Include(r => r.Responses)
                .ToList();
        }
        #endregion

        #region Post CRUD
        // Add Post
        public void AddPost(Post post)
        {
            if (post != null)
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
        }
        // Edit Post
        public void EditPost(Post post)
        {
            Post editPost = context.Posts.Where(p => p.PostId == post.PostId).Single();
            if (editPost != null)
            {
                editPost.Title = post.Title;
                editPost.Message = post.Message;
                editPost.UserImage = post.UserImage;
                editPost.WebURL = post.WebURL;
            }
            context.SaveChanges();
        }
        // Delete Post
        public void DeletePost(int postId)
        {
            Post post = context.Posts.Where(p => p.PostId == postId).Single();
            context.Posts.Remove(post);
            context.SaveChanges();
        }
        // Get Post
        public Post GetPost(int postID)
        {
            return context.Posts
                .Include(r => r.Room)
                .Include(u => u.User)
                .Include(r => r.Responses)
                .Single(x => x.PostId == postID);
        }
        // Get all posts associated with UserId
        public List<Post> GetPostsByUser(string userId)
        {
            List<Post> posts = context.Posts.Where(p => p.UserId == userId).ToList();
            return posts;
        }
        // Get All Posts
        public List<Post> GetAllPosts(int roomID)
        {
            return context.Posts
                .Include(r => r.Room)
                .Include(u => u.User)
                .Include(r => r.Responses)
                .Where(x => x.RoomId == roomID)
                .ToList();
        }
        // Get most recent post
        public Post GetNewestPost()
        {
            Post post = context.Posts.OrderByDescending(x => x.DatePosted).First();
            return post;
        }
        // Get post with most responses today
        public Post GetMostPopularPostToday()
        {
            var limit = DateTime.Now.AddDays(-1);
            List<Post> postsToday = context.Posts.Where(x => x.DatePosted > limit).Include(r => r.Responses).ToList();
            if (postsToday.Count > 0)
            {
                Post post = postsToday.OrderByDescending(x => x.Responses.Count).First();
                return post;
            }
            return null;
        }
        // Get post associated with specified EventId
        public int GetPostIdFromEventId(int newsId)
        {
            var post = context.Posts.Where(n => n.EventId == newsId).SingleOrDefault();
            if (post == null)
            {
                return 0;
            }
            return post.PostId;
        }
        // Get post assocated with photo of the day
        public int GetPostIdFromPhotoDate(DateTime date)
        {
            var post = context.Posts.Where(p => p.POTDDate == date).SingleOrDefault();
            if (post == null)
            {
                return 0;
            }
            return post.PostId;
        }
        #endregion

        #region Response CRUD
        // Add Response
        public void AddResponse(Response response)
        {
            if (response != null)
            {
                context.Responses.Add(response);
                context.SaveChanges();
            }
        }
        // Edit response
        public void EditResponse(Response response)
        {
            Response editResponse = context.Responses.Where(p => p.ResponseId == response.ResponseId).Single();
            if (editResponse != null)
            {
                editResponse.Message = response.Message;
            }
            context.SaveChanges();
        }
        // Delete response
        public void DeleteResponse(int responseId)
        {
            var response = context.Responses.Where(r => r.ResponseId == responseId).Single();
            context.Responses.Remove(response);
            context.SaveChanges();
        }
        // Get Response by ReponseId
        public Response GetResponse(int responseId)
        {
            return context.Responses
                .Include(u => u.User)
                .Include(p => p.Post)
                .Where(i => i.ResponseId == responseId)
                .Single();
        }
        // Get responses associated with specific UserId
        public List<Response> GetResponsesByUser(string userId)
        {
            var responses = context.Responses.Where(r => r.UserId == userId).ToList();
            return responses;
        }
        // Get All Responses
        public List<Response> GetAllResponses(int postId)
        {
            return context.Responses
                .Include(u => u.User)
                .Where(x => x.PostId == postId)
                .ToList();
        }
        #endregion

        #region Other
        public int GetPostCountPerRoom(int roomId)
        {
            int count = context.Posts.Count(x => x.RoomId == roomId);
            return count;
        }
        #endregion
    }
}
