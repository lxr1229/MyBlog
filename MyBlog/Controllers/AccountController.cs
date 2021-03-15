using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    public class AccountController : Controller
    {
        //用于提供持久性存储的用户信息
        private UserManager<BlogUser> _userManager;
        private SignInManager<BlogUser> _signInManager;
        public AccountController(UserManager<BlogUser> userManager, SignInManager<BlogUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user != null)
            {
                user.DateLastLogin = DateTime.Now;
                await _userManager.UpdateAsync(user);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
            }
            ModelState.AddModelError("", "用户名或密码错误");

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new BlogUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    DateCreated = DateTime.Now
                };        
                //创建用户
                var result = await _userManager.CreateAsync(user,model.Password);
                //如果成功则返回首页
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
