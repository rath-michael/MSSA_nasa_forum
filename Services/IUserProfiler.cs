using Week15Project.Models;

namespace Week15Project.Services
{
    public interface IUserProfiler
    {
        public User CurrentUser { get; set; }
        public int PostCount { get; }
        public int ResponseCount { get; }
        public DateTime JoinDate { get; }
        public void AssignUser(string username);
    }
}
