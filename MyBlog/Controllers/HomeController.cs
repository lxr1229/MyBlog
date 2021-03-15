using Microsoft.AspNetCore.Mvc;
using MyBlog.Service;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _post;

        public HomeController(IPostService post)
        {
            _post = post;
        }

        public IActionResult Index()
        {
            var posts = _post.GetPostList();
            return View(posts);
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
