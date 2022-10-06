using Week15Project.Models;

namespace Week15Project.Services
{
    public class ForumRepository : IForumRepository
    {
        private ForumProjectContext context;
        public ForumRepository(ForumProjectContext context)
        {
            this.context = context;
        }

        #region User CRUD
        // Add User
        //public void AddUser(User user)
        //{
        //    if (user != null)
        //    {
        //        context.Users.Add(user);
        //        context.SaveChanges();
        //    }
        //}
        // Get User
        // Get All Users
        // Update User
        // Delete User
        #endregion


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
            return context.Rooms.Where(x => x.RoomId == roomId).Single();
        }
        // Get All Rooms
        public List<Room> GetAllRooms()
        {
            return context.Rooms.ToList();
        }
        // Update Room
        // Delete Room
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
            var post = context.Posts.FirstOrDefault(x => x.PostId == postID);
            return post;
        }
        // Get All Posts
        public List<Post> GetAllPosts(int roomID)
        {
            var posts = context.Posts.Where(x => x.RoomId == roomID).ToList();
            return posts;
        }
        // Update Post
        // Delete Post
        #endregion


        #region Response CRUD
        // Add Response
        public void AddResponse(Response message)
        {
            if (message != null)
            {
                context.Responses.Add(message);
                context.SaveChanges();
            }
        }
        // Get Response
        // Get All Responses
        public List<Response> GetAllResponses(int postId)
        {
            var responses = context.Responses.Where(x => x.PostId == postId).ToList();
            return responses;
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
