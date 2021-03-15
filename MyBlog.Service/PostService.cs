using MyBlog.Data;
using MyBlog.Repository;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _post;

        public PostService(IPostRepository post)
        {
            _post = post;
        }

        public List<Post> GetPostList()
        {
            return _post.GetList().Result;
        }
        public Post GetPost(int id)
        {
            return _post.GetAsync(id).Result;
        }
        public bool AddPost(Post post)
        {
            return _post.AddAsync(post).Result;
        }
        public bool UpdatePost(Post post)
        {
            return _post.UpdateAsync(post).Result;
        }
        public bool DeletePost(Post post)
        {
            return _post.DeleteAsync(post).Result;
        }
    }
}
