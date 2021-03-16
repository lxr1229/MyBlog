using MyBlog.Data;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public interface IPostService
    {
        List<Post> GetPostList(); 
        Post GetPost(int id);
        int AddPost(Post post);
        int UpdatePost(Post post);
        bool DeletePost(Post post);
    }
}
