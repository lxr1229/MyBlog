using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Service;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _post;
        private readonly IPostTagService _postTag;
        private readonly ITagService _tag;
        private readonly ICategoryService _category;
        private readonly IMapper _mapper;
        private UserManager<BlogUser> _userManager;

        public HomeController(IPostService post, IPostTagService postTag, ITagService tag, ICategoryService category,IMapper mapper, UserManager<BlogUser> userManager)
        {
            _post = post;
            _mapper = mapper;
            _postTag = postTag;
            _tag = tag;
            _category = category;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var posts = _post.GetPostList().OrderByDescending(o=>o.DateCreated).ToList();

            var model = _mapper.Map<List<Post>, List<PostListViewModel>>(posts);

            foreach (var item in model)
            {
                item.UserName = _userManager.FindByIdAsync(item.UserId).Result.UserName;
                item.CategoryName = _category.GetCategory(o => o.CategoryId == item.CategoryId).CategoryName;
                item.PostTagList = _mapper.Map<List<PostTag>, List<PostTagViewModel>>(_postTag.GetPostTagList(o=>o.PostId == item.PostId));
                foreach (var postTag in item.PostTagList)
                {
                    postTag.TagName = _tag.GetTag(o => o.TagId == postTag.TagId).TagName;
                }
            }
            return View(model);
        }

        public IActionResult Privacy() 
        {
            var posts = _post.GetPostList();

            return View(posts);
        }


        public IActionResult Post(int id)
        {
            var post = _post.GetPost(id);

            return View(post);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _post.DeletePost(_post.GetPost(id));
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
