using MyBlog.Data;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public interface ICategoryService
    {
        List<Category> GetCategoryList();
        Category GetCategory(int id);
        bool AddCategory(Category cate);
        bool UpdateCategory(Category cate);
        bool DeleteCategory(Category cate);
    }
}
