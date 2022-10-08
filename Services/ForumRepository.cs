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
            return context.Rooms
                .Include(p => p.Posts)
                .ToList();
        }
        // Update Room
        // Delete Room
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
        // Get All Users
        public List<User> GetAllUsers()
        {
            return context.Users
                .Include(p => p.Posts)
                .Include(r => r.Responses)
                .ToList();
        }
        // Update User
        // Delete User
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
        // Get Post
        public Post GetPost(int postID)
        {
            return context.Posts
                .Include(r => r.Room)
                .Include(u => u.User)
                .Include(r => r.Responses)
                .FirstOrDefault(x => x.PostId == postID);
        }
        // Get All Posts
        public List<Post> GetAllPosts(int roomID)
        {
            return context.Posts
                .Include(r => r.Room)
                .Include(u => u.User)
                .Include(r => r.Responses)
                .Where(x => x.RoomId == roomID)
                .ToList(); ;
        }
        // Update Post
        // Delete Post
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
        // Get Response
        public Response GetResponse(int responseId)
        {
            return context.Responses
                .Include(u => u.User)
                .Include(p => p.Post)
                .Where(i => i.ResponseId == responseId)
                .FirstOrDefault();
        }
        // Get All Responses
        public List<Response> GetAllResponses(int postId)
        {
            return context.Responses
                .Include(u => u.User)
                .Where(x => x.PostId == postId)
                .ToList();
        }
        // Update Response
        // Delete Response
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
