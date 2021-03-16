namespace MyBlog.Service
{
    public class BaseResponse
    { 
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
