using MyBlog.Data;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public interface IPostService
    {
        List<Post> GetPostList(); 
        Post GetPost(int id);
        bool AddPost(Post post);
        bool UpdatePost(Post post);
        bool DeletePost(Post post);
    }
}
