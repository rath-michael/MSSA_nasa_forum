using Week15Project.Models;

namespace Week15Project.Services
{
    public interface IRoomProfiler
    {
        List<Room> GetAllRooms();
        int GetPostCountPerRoom(int roomId);
    }
}
