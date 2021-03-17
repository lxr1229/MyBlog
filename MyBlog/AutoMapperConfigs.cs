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
            CreateMap<Post, PostListViewModel>();
            CreateMap<PostTag, PostTagViewModel>();
            CreateMap<Tag, TagViewModel>();
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
