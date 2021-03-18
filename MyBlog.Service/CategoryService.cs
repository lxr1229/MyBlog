using MyBlog.Data;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyBlog.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _cate;
        private readonly IBaseRepository<Category> _repo;

        public CategoryService(ICategoryRepository cate, IBaseRepository<Category> repo)
        {
            _cate = cate;
            _repo = repo;
        }

        public List<Category> GetCategoryList()
        {
            return _cate.GetList().Result;
        }
        public Category GetCategory(int id)
        {
            return _cate.GetAsync(id).Result;
        }
        public Category GetCategory(Expression<Func<Category, bool>> predicate)
        {
            return _cate.GetAsync(predicate).Result;
        }
        public DataResponse<int> AddCategory(Category cate)
        {
            if (_cate.GetListAsync(o => o.CategoryName == cate.CategoryName).Result.Count > 0)
            {
                return new DataResponse<int> { Success = false, Message = "分类名字有重复" };
            }

            cate.DateCreated = DateTime.Now;
            var result = _cate.AddAsync(cate).Result;

            if (result)
            {
                return new DataResponse<int> { Success = true, data = cate.CategoryId , Message = "操作成功" };
            }
            else
            {
                return new DataResponse<int> { Success = false, Message = "操作失败" };
            }
        }
        public DataResponse<int> UpdateCategory(Category cate)
        {
            if (_cate.GetListAsync(o => o.CategoryName == cate.CategoryName).Result.Count > 0)
            {
                return new DataResponse<int> { Success = false, Message = "分类名字有重复" };
            }

            cate.DateCreated = _cate.GetAsync(o=>o.CategoryId == cate.CategoryId).Result.DateCreated;

            var result = _cate.UpdateAsync(cate).Result;
            if (result)
            {
                return new DataResponse<int> { Success = true, data = cate.CategoryId , Message = "操作成功" };
            }
            else
            {
                return new DataResponse<int> { Success = false, Message = "操作失败" };
            }
        }
        public bool DeleteCategory(Category cate)
        {
            return _cate.DeleteAsync(cate).Result;
        }
    }
}
