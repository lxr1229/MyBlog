using System;

namespace MyBlog.Models
{
    public class PostEditViewModel 
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentMarkDown { get; set; }
        public string UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? PostTypeId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
