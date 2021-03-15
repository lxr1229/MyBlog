using MyBlog.Data;
using MyBlog.Repository;
using System.Collections.Generic;

namespace MyBlog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _cate;

        public CategoryService(ICategoryRepository cate)
        {
            _cate = cate;
        }

        public List<Category> GetCategoryList()
        {
            return _cate.GetList().Result;
        }
        public Category GetCategory(int id)
        {
            return _cate.GetAsync(id).Result;
        }
        public bool AddCategory(Category cate)
        {
            return _cate.AddAsync(cate).Result;
        }
        public bool UpdateCategory(Category cate)
        {
            return _cate.UpdateAsync(cate).Result;
        }
        public bool DeleteCategory(Category cate)
        {
            return _cate.DeleteAsync(cate).Result;
        }
    }
}
