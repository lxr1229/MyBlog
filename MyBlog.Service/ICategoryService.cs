using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public interface ICategoryService
    {
        List<Category> GetCategoryList();
        Category GetCategory(int id);
        Category GetCategory(Expression<Func<Category, bool>> predicate);
        DataResponse<int> AddCategory(Category cate);
        DataResponse<int> UpdateCategory(Category cate);
        bool DeleteCategory(Category cate);
    }
}
