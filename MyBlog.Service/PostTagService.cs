using MyBlog.Data;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public class PostTagService : IPostTagService
    {
        private readonly IPostTagRepository _postTag;

        public PostTagService(IPostTagRepository postTag)
        {
            _postTag = postTag;
        }

        public List<PostTag> GetPostTagList()
        {
            return _postTag.GetList().Result;
        }
        public List<PostTag> GetPostTagList(Expression<Func<PostTag, bool>> predicate)
        {
            return _postTag.GetListAsync(predicate).Result;
        }

        public PostTag GetPostTag(int id)
        {
            return _postTag.GetAsync(id).Result;
        }
        public bool AddPostTag(PostTag postTag, bool IsSave)
        {
            return _postTag.AddAsync(postTag, IsSave).Result;
        }
        public bool UpdatePostTag(PostTag postTag)
        {
            return _postTag.UpdateAsync(postTag).Result;
        }
        public bool DeletePostTag(PostTag postTag)
        {
            return _postTag.DeleteAsync(postTag).Result;
        }
        public bool DeletePostTagList(Expression<Func<PostTag, bool>> predicate)
        {
            return _postTag.DeleteAsync(predicate).Result;
        }
    }
}
