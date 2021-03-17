using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using MyBlog.Service;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

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

        public IActionResult Index(PostSearchViewModel model)
        {
            if (model.PageIndex == 0) model.PageIndex = 1;
            if (model.PageSize == 0) model.PageSize = 10;

            IPagedList<Post> posts = _post.GetPostList().OrderByDescending(o=>o.DateCreated).ToList().ToPagedList(model.PageIndex,model.PageSize);
            // map to IEnumerable
            IEnumerable<PostListViewModel> postList = _mapper.Map<IEnumerable<PostListViewModel>>(posts);
            // create an instance of StaticPagedList with the mapped IEnumerable and original IPagedList metadata
            IPagedList<PostListViewModel> postListViewModel = new StaticPagedList<PostListViewModel>(postList, posts.GetMetaData());

            foreach (var item in postListViewModel)
            {
                item.UserName = _userManager.FindByIdAsync(item.UserId).Result.UserName;
                item.CategoryName = _category.GetCategory(o => o.CategoryId == item.CategoryId).CategoryName;
                item.PostTagList = _mapper.Map<List<PostTag>, List<PostTagViewModel>>(_postTag.GetPostTagList(o=>o.PostId == item.PostId));
                foreach (var postTag in item.PostTagList)
                {
                    postTag.TagName = _tag.GetTag(o => o.TagId == postTag.TagId).TagName;
                }
            }

            var postInfoModel = new PostInfoViewModel
            { 
                PostList = postListViewModel, 
                TagList = _mapper.Map<List<Tag>, List<TagViewModel>>(_tag.GetTagList()) ,
                CategoryList = _mapper.Map<List<Category>, List<CategoryViewModel>>(_category.GetCategoryList())
            };

            return View(postInfoModel);
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
