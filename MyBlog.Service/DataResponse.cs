namespace MyBlog.Service
{
    public class DataResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T data { get; set; }
    }
}
