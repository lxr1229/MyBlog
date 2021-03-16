using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public interface ITagService
    {
        List<Tag> GetTagList();
        Tag GetTag(int id);
        Tag GetTag(Expression<Func<Tag, bool>> predicate);
        bool AddTag(Tag tag, bool isCommit);
        bool UpdateTag(Tag tag);
        bool DeleteTag(Tag tag);
    }
}
