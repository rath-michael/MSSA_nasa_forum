namespace Week15Project.Models.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public List<Response> Responses { get; set; }
    }
}
