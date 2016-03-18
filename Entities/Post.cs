using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities
{
    public class Post
    {       
        public string Avatar { get; set; }

        public string AuthorName { get; set; }

        public Guid PostId { get; set; }

        public string Text { get; set; }
        
        public string NamePost { get; set; }

        public string Source { get; set; }

        public DateTime CreatedTime { get; set; }

        public Guid AccountId { get; set; }

        public string [] Tags { get; set; }

        public int Rating { get; set; }

    }
}
