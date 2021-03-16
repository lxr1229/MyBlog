using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MyBlog.Models
{
    public class PostListViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContentString
        {
            get
            {
                return ParseTags(Content);
            }
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ConstantsEnum.PostType PostTypeId { get; set; }
        public int Views { get; set; }
        public List<PostTagViewModel> PostTagList { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;


        /// <summary>
        /// 移除HTML标签   
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string ParseTags(string HTMLStr)
        {
            return Regex.Replace(HTMLStr, "<[^>]*>", "");
        }
    }

    public class PostEditViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "请填写标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请填写内容")]
        public string Content { get; set; }

        public string ContentMarkDown { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "请选择分类")]
        public int CategoryId { get; set; }

        public int PostTypeId { get; set; }

        public string TagString { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
