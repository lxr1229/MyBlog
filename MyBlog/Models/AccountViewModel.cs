using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "密码与确认密码不一致，请重新输入.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
