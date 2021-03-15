using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity>
    {
        #region Add
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(TEntity entity, bool IsCommit = true);
        /// <summary>
        /// 增加多条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddListAsync(List<TEntity> entities, bool IsCommit = true);
        #endregion

        #region Delete
        /// <summary>
        /// 删除一条记录（根据Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity, bool IsCommit = true);
        /// <summary>
        /// 删除一条或多条记录（根据Lamda表达式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true);
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteListAsync(List<TEntity> entities, bool IsCommit = true);
        #endregion

        #region Update
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, bool IsCommit = true);
        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateListAsync(List<TEntity> entities, bool IsCommit = true);
        #endregion

        #region Get
        /// <summary>
        /// 获取一条记录（根据Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(int id);
        /// <summary>
        /// 获取一条记录（根据Lamda表达式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取多条记录
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetList();
        /// <summary>
        /// 获取多条记录（根据Lamda表达式）
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        /// <summary>
        /// 验证是否存在相同记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true);
        /// <summary>
        /// 保存记录至数据库
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
    }
}
