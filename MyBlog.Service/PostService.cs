using MyBlog.Data;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        public List<Post> GetPostList(Expression<Func<Post, bool>> predicate)
        {
            return _post.GetListAsync(predicate).Result;
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
        public bool UpdatePostViews(Post post)
        {
            post.Views++;

            return _post.UpdateAsync(post).Result;
        }
        public bool DeletePost(Post post)
        {
            return _post.DeleteAsync(post).Result;
        }

        public int GetCountUserPosts(string userID)
        {
            return _post.GetEntityCountAsync(o => o.UserId == userID).Result;
        }
        public int? GetCountUserViews(string userID)
        {
            return _post.GetSum(o => o.UserId == userID, o=>o.Views);
        }
    }
}
