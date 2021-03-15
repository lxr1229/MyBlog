using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
