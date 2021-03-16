namespace MyBlog.Models
{
    public class PostTagViewModel
    {
        public int PostTagId { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}
