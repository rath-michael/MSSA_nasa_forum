using System;
using System.Collections.Generic;

namespace Week15Project.Models
{
    public partial class Room
    {
        public Room()
        {
            Posts = new HashSet<Post>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string RoomDescription { get; set; }
        public string RoomImage { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
