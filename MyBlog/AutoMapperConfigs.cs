using AutoMapper;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<PostEditViewModel, Post>();
            CreateMap<Post, PostEditViewModel>();
        }
    }
}
