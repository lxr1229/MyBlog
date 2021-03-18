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
        private readonly ICategoryService _category;
        private readonly ITagService _tag;
        private readonly IPostTagService _postTag;
        private readonly IMapper _mapper;
        private UserManager<BlogUser> _userManager;

        public PostController(IPostService post, IUnitOfWork unitOfWork, ICategoryService category, ITagService tag, IPostTagService postTag, IMapper mapper, UserManager<BlogUser> userManager)
        {
            _post = post;
            _unitOfWork = unitOfWork;
            _category = category;
            _tag = tag;
            _postTag = postTag;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var post = _post.GetPost(id);
            _post.UpdatePostViews(post);

            var model = _mapper.Map<Post, PostListViewModel>(post);

            model.PostTagList = _mapper.Map<List<PostTag>, List<PostTagViewModel>>(_postTag.GetPostTagList(o => o.PostId == id));
            model.CategoryName = _category.GetCategory(o => o.CategoryId == model.CategoryId).CategoryName;
            model.UserName = _userManager.FindByIdAsync(model.UserId).Result.UserName;
            model.CountUserPosts = _post.GetCountUserPosts(model.UserId);
            model.CountUserViews = _post.GetCountUserViews(model.UserId).Value;
            foreach (var postTag in model.PostTagList)
            {
                postTag.TagName = _tag.GetTag(o => o.TagId == postTag.TagId).TagName;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.CategoryList = _category.GetCategoryList();

            if (id.HasValue)
            {
                var post = _post.GetPost(id.Value);
                var model = _mapper.Map<Post, PostEditViewModel>(post);

                var tagList = _postTag.GetPostTagList(o => o.PostId == id.Value);
                var tagNameList = new List<string>();
                if (tagList.Count > 0)
                {
                    foreach (var item in tagList)
                    {
                        tagNameList.Add(_tag.GetTag(item.TagId).TagName);
                    }
                    model.TagString = string.Join(",", tagNameList);
                }

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
            if (model.PostId.HasValue)
            {
                var postId = _post.UpdatePost(post);

                _postTag.DeletePostTagList(o => o.PostId == post.PostId);

                foreach (var item in tagList)
                {
                    _postTag.AddPostTag(new PostTag { PostId = post.PostId, TagId = _tag.GetTag(o => o.TagName == item).TagId }, false);
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
