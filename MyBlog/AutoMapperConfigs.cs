using AutoMapper;
using MyBlog.Data;
using MyBlog.Models;
using System.Collections.Generic;

namespace MyBlog
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<PostEditViewModel, Post>();
            CreateMap<Post, PostEditViewModel>();
            CreateMap<Post, PostListViewModel>();
            CreateMap<PostTag, PostTagViewModel>();
        }
    }
}
