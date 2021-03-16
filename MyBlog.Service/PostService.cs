using MyBlog.Data;
using MyBlog.Repository;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _post;
        private readonly BlogDbContext _context;

        public PostService(IPostRepository post, BlogDbContext context)
        {
            _post = post;
            _context = context;
        }

        public List<Post> GetPostList()
        {
            return _post.GetList().Result;
        }
        public Post GetPost(int id)
        {
            return _post.GetAsync(id).Result;
        }
        public int AddPost(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();

            return post.PostId;
        }
        public int UpdatePost(Post post)
        {
            _context.Update(post);
            _context.SaveChanges();

            return post.PostId;
        }
        public bool DeletePost(Post post)
        {
            return _post.DeleteAsync(post).Result;
        }
    }
}
