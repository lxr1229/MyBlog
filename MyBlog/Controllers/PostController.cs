using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Repository;
using MyBlog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _post;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _cate;
        private readonly ITagService _tag;
        private readonly IPostTagService _postTag;
        private readonly IMapper _mapper;
        private UserManager<BlogUser> _userManager;

        public PostController(IPostService post, IUnitOfWork unitOfWork, ICategoryService cate, ITagService tag, IPostTagService postTag, IMapper mapper, UserManager<BlogUser> userManager)
        {
            _post = post;
            _unitOfWork = unitOfWork;
            _cate = cate;
            _tag = tag;
            _postTag = postTag;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.CategoryList = _cate.GetCategoryList();

            if (id.HasValue)
            {
                var post = _post.GetPost(id.Value);
                var model = _mapper.Map<Post, PostEditViewModel>(post);

                return View(model);
            }
            return View(new PostEditViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(PostEditViewModel model)
        {
            BlogUser user = await _userManager.GetUserAsync(HttpContext.User);

            var post = _mapper.Map<PostEditViewModel, Post>(model);
            post.UserId = user.Id;

            var tagList = new List<string>();
            // 添加标签至数据库
            if (!string.IsNullOrEmpty(model.TagString))
            {
                tagList = model.TagString.Split(',').ToList();
                foreach (var item in tagList)
                {
                    if (_tag.GetTag(o => o.TagName == item) == null)
                    {
                        _tag.AddTag(new Tag { TagName = item, DateCreated = DateTime.Now }, false);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
            }

            // 添加文章至数据库库
            if (model.PostId > 0)
            {
                var postId = _post.UpdatePost(post);

                _postTag.DeletePostTagList(o => o.PostId == postId);

                foreach (var item in tagList)
                {
                    _postTag.AddPostTag(new PostTag { PostId = postId, TagId = _tag.GetTag(o => o.TagName == item).TagId }, false);
                }

            }
            else
            {
                var postId = _post.AddPost(post);

                foreach (var item in tagList)
                {
                    _postTag.AddPostTag(new PostTag { PostId = postId, TagId = _tag.GetTag(o => o.TagName == item).TagId }, false);
                }
            }

            var result = await _unitOfWork.SaveChangesAsync();

            if (result)
            {
                return Json(new BaseResponse { Success = true, Message = "操作成功" });
            }
            else
            {
                return Json(new BaseResponse { Success = true, Message = "操作失败" });
            }
        }
    }
}
