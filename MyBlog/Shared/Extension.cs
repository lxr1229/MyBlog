using AutoMapper;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using X.PagedList;

namespace MyBlog.Shared
{
    public class Extension
    {
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
}
