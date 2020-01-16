using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagText { get; set; }
        public List<Post> Posts { get; set; }
    }
}
