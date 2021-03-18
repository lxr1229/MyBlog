using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public interface IPostService
    {
        List<Post> GetPostList();
        Post GetPost(int id);
        List<Post> GetPostList(Expression<Func<Post, bool>> predicate);
        int AddPost(Post post);
        bool UpdatePost(Post post);
        bool UpdatePostViews(Post post);
        bool DeletePost(Post post);
        int GetCountUserPosts(string userID);
        int? GetCountUserViews(string userID);
    }
}
