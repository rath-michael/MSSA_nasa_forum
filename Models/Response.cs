using System;
using System.Collections.Generic;

namespace Week15Project.Models
{
    public partial class Response
    {
        public int ResponseId { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime DatePosted { get; set; }
        public string Message { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
    }
}
