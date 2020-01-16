using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public bool Approved { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public int CategoryId { get; set; }     
        public string CategoryName { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
        public string UserId { get; set; }
    }
}
