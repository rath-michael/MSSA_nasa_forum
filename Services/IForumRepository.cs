using Week15Project.Models;

namespace Week15Project.Services
{
    public interface IForumRepository
    {
        //void AddUser(User user);
        void AddRoom(Room room);
        void AddPost(Post post);
        void AddResponse(Response message);
        //List<User> GetAllUsers();
        List<Room> GetAllRooms();
        List<Post> GetAllPosts(int roomID);
        List<Response> GetAllResponses(int postId);
        User GetUser(string id);
        Room GetRoom(int id);
        Post GetPost(int id);
        int GetNewestPostId();
        int GetMostPopularPostId();
        //Response GetResponse(int id);
        //void UpdateUser(User user);
        //void UpdateRoom(Room room);
        //void UpdatePost(Post post);
        //void UpdateResponse(Response message);
        //void DeleteUser(int id);
        //void DeleteRoom(int id);
        //void DeletePost(int id);
        //void DeleteResponse(int id);
        int GetPostCountPerRoom(int roomId);
    }
}
