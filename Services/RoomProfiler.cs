using Week15Project.Models;

namespace Week15Project.Services
{
    public class RoomProfiler : IRoomProfiler
    {
        private IForumRepository repository;
        public RoomProfiler(IForumRepository repository)
        {
            this.repository = repository;
        }

        public List<Room> GetAllRooms()
        {
            return repository.GetAllRooms();
        }
    }
}
