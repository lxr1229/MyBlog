using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Service;
using System;
using System.Collections.Generic;

namespace MyBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _category;
        private readonly IPostService _post;
        private readonly IPostTagService _postTag;
        private readonly IMapper _mapper;

        public AdminController(ICategoryService category, IPostTagService postTag, IMapper mapper, IPostService post)
        {
            _category = category;
            _mapper = mapper;
            _post = post;
            _postTag = postTag;
        }

        public IActionResult Index()
        {
            var list = _category.GetCategoryList();
            var model = _mapper.Map<List<Category>, List<CategoryViewModel>>(list);

            return View(model);
        }

        public IActionResult CategoryList()
        {
            var list = _category.GetCategoryList();
            var model = _mapper.Map<List<Category>, List<CategoryViewModel>>(list);

            return PartialView("_CategoryList", model);
        }
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            if (_post.GetPostList(o => o.CategoryId == id).Count > 0)
            {
                return Json(new BaseResponse { Success = false, Message = "有博文在此分类下，无法删除" });
            }

            var result = _category.DeleteCategory(_category.GetCategory(id));
            if (result)
            {
                return Json(new BaseResponse { Success = true, Message = "删除成功" });
            }
            return Json(new BaseResponse { Success = false, Message = "删除失败" });
        }
        public IActionResult GetCategoryEditLayer(int? id)
        {
            if (id.HasValue)
            {
                var item = _category.GetCategory(id.Value);
                var model = _mapper.Map<Category, CategoryViewModel>(item);

                return PartialView("_CategoryEdit", model);
            }
            else
            {
                return PartialView("_CategoryEdit", new CategoryViewModel());
            }
        }
        [HttpPost]
        public ActionResult EditCategory(CategoryEditViewModel model)
        {
            var item = _mapper.Map<CategoryEditViewModel, Category>(model);

            var result = new DataResponse<int>();
            if (model.CategoryId.Value == 0)
            {
                result = _category.AddCategory(item);
            }
            else
            {
                result = _category.UpdateCategory(item);
            }

            //if (result.Success)
            //{
            //    var data = _mapper.Map<Category, CategoryViewModel>(_category.GetCategory(result.data));

            //    return PartialView("_CategoryListItem", data);
            //}
            //else
            //{
            //    return Json(result);
            //}
            return Json(result);
        }

        public IActionResult PostList()
        {
            var list = _post.GetPostList();
            var model = _mapper.Map<List<Post>, List<PostListViewModel>>(list);

            return PartialView("_PostList", model);
        }
        public IActionResult DeletePost(int id)
        {
            var result = _post.DeletePost(_post.GetPost(id));
            if (result)
            {
                _postTag.DeletePostTagList(o => o.PostId == id);
                return Json(new BaseResponse { Success = true, Message = "删除成功" });
            }
            return Json(new BaseResponse { Success = false, Message = "删除失败" });
        }
    }
}
