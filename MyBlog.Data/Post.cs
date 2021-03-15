using System;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentMarkDown { get; set; }
        public string UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? PostTypeId { get; set; }
        public int Views { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
