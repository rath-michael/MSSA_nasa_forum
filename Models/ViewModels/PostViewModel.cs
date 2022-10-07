namespace Week15Project.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public User User { get; set; }
        public List<Response> Responses { get; set; }
    }
}
