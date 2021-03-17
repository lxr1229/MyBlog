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
    public class AuthorController : Controller
    {
        private readonly IPostService _post;
        private readonly ICategoryService _category;
        private readonly ITagService _tag;
        private readonly IPostTagService _postTag;
        private readonly IMapper _mapper;
        private UserManager<BlogUser> _userManager;

        public AuthorController(IPostService post, ICategoryService category, ITagService tag, IPostTagService postTag, IMapper mapper, UserManager<BlogUser> userManager)
        {
            _post = post;
            _category = category;
            _tag = tag;
            _postTag = postTag;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index(string id, PostSearchViewModel model)
        {
            if (model.PageIndex == 0) model.PageIndex = 1;
            if (model.PageSize == 0) model.PageSize = 10;

            IPagedList<Post>  posts = _post.GetPostList(o=>o.UserId == id).OrderByDescending(o => o.DateCreated).ToList().ToPagedList(model.PageIndex, model.PageSize); ;

            IEnumerable<PostListViewModel> postList = _mapper.Map<IEnumerable<PostListViewModel>>(posts);
            // create an instance of StaticPagedList with the mapped IEnumerable and original IPagedList metadata
            IPagedList<PostListViewModel> postListViewModel = new StaticPagedList<PostListViewModel>(postList, posts.GetMetaData());

            foreach (var item in postListViewModel)
            {
                item.PostTagList = _mapper.Map<List<PostTag>, List<PostTagViewModel>>(_postTag.GetPostTagList(o => o.PostId == item.PostId));
                foreach (var postTag in item.PostTagList)
                {
                    postTag.TagName = _tag.GetTag(o => o.TagId == postTag.TagId).TagName;
                }
                item.CategoryName = _category.GetCategory(o => o.CategoryId == item.CategoryId).CategoryName;
            }

            var authorModel = new PostAuthorViewModel { PostList = postListViewModel };
            authorModel.UserName = _userManager.FindByIdAsync(id).Result.UserName;

            authorModel.CountUserPosts = _post.GetCountUserPosts(id);
            authorModel.CountUserViews = _post.GetCountUserViews(id).Value;

            return View(authorModel);
        }
    }
}
