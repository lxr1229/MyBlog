using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 保存记录至数据库
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
