using MyBlog.Data;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tag;

        public TagService(ITagRepository tag)
        {
            _tag = tag;
        }

        public List<Tag> GetTagList()
        {
            return _tag.GetList().Result;
        }
        public Tag GetTag(int id)
        {
            return _tag.GetAsync(id).Result;
        }
        public Tag GetTag(Expression<Func<Tag, bool>> predicate)
        {
            return _tag.GetAsync(predicate).Result;
        }

        public bool AddTag(Tag tag, bool isCommit)
        {
            return _tag.AddAsync(tag, isCommit).Result;
        }
        public bool UpdateTag(Tag tag)
        {
            return _tag.UpdateAsync(tag).Result;
        }
        public bool DeleteTag(Tag tag)
        {
            return _tag.DeleteAsync(tag).Result;
        }
    }
}
