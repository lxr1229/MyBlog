namespace MyBlog.Data
{
    public class PostTag
    {
        public int PostTagId { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
