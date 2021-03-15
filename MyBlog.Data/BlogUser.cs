using Microsoft.AspNetCore.Identity;
using System;

namespace MyBlog.Data
{
    public class BlogUser : IdentityUser
    {
        public DateTime DateLastLogin { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
