using System;
using System.Collections.Generic;

namespace Week15Project.Models
{
    public partial class Post
    {
        public Post()
        {
            Responses = new HashSet<Response>();
        }

        public int PostId { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime DatePosted { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public byte[]? UserImage { get; set; }
        public bool Locked { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Response> Responses { get; set; }
    }
}
