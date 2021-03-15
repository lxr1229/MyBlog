using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Service;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _post;
        private readonly ICategoryService _cate;
        private readonly IMapper _mapper;

        public PostController(IPostService post, ICategoryService cate,IMapper mapper)
        {
            _post = post;
            _cate = cate;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.CategoryList  = _cate.GetCategoryList();

            if (id.HasValue)
            {
                var post = _post.GetPost(id.Value);
                var model = _mapper.Map<Post, PostEditViewModel>(post);

                return View(model);
            }
            return View(new PostEditViewModel());
        }

        [HttpPost]
        public IActionResult Edit(PostEditViewModel model)
        {
            var result = false;
            var post = _mapper.Map<PostEditViewModel, Post>(model);

            if (post.PostId > 0)
            {
                result = _post.UpdatePost(post);
            }
            else
            {
                result = _post.AddPost(post);
            }

            if (result)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View(post);
            }
        }
    }
}
