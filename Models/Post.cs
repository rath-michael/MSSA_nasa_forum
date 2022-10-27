using System;
using System.Collections;
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
        public string? UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        //public string? UserImage { get; set; }
        public string? MediaURL { get; set; }
        public string? MediaType { get; set; }
        public string? WebURL { get; set; }
        public int EventId { get; set; } // associates post with news event
        public DateTime POTDDate { get; set; } // associates post with nasa potd
        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Response> Responses { get; set; }
        public virtual User? User { get; set; }
    }
}
