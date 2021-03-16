using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public interface IPostTagService
    {
        List<PostTag> GetPostTagList();
        List<PostTag> GetPostTagList(Expression<Func<PostTag, bool>> predicate);
        PostTag GetPostTag(int id);
        bool AddPostTag(PostTag postTag, bool IsSave);
        bool UpdatePostTag(PostTag postTag);
        bool DeletePostTag(PostTag postTag);
        bool DeletePostTagList(Expression<Func<PostTag, bool>> predicate);
    }
}
