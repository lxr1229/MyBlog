using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BlogDbContext _context;

        public BaseRepository(BlogDbContext context)
        {
            _context = context;
        }

        #region Add
        public async Task<bool> AddAsync(TEntity entity, bool IsSave = true)
        {
            await _context.Set<TEntity>().AddAsync(entity);

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }

        public async Task<bool> AddListAsync(List<TEntity> entities, bool IsSave = true)
        {
            if (entities == null || entities.Count == 0)
            {
                return await Task.Run(() => false);
            }

            await _context.Set<TEntity>().AddRangeAsync(entities);

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteAsync(TEntity entity, bool IsSave = true)
        {
            if (entity == null)
            {
                return await Task.Run(() => false);
            }
            _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Remove(entity);

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool IsSave = true)
        {
            IQueryable<TEntity> entry = (predicate == null) ? _context.Set<TEntity>().AsQueryable() : _context.Set<TEntity>().Where(predicate);
            List<TEntity> entities = entry.ToList();
            if (entities == null || entities.Count == 0)
            {
                return await Task.Run(() => false);
            }
            entities.ForEach(item =>
            {
                _context.Set<TEntity>().Attach(item);
                _context.Set<TEntity>().RemoveRange(item);
            });

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }

        public async Task<bool> DeleteListAsync(List<TEntity> entities, bool IsSave = true)
        {
            if (entities == null || entities.Count == 0)
            {
                return await Task.Run(() => false);
            }
            entities.ToList().ForEach(item =>
            {
                _context.Set<TEntity>().Attach(item);
                _context.Set<TEntity>().Remove(item);
            });

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }
        #endregion

        #region Update
        public async Task<bool> UpdateAsync(TEntity entity, bool IsSave = true)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }

        public async Task<bool> UpdateListAsync(List<TEntity> entities, bool IsSave = true)
        {
            if (entities == null || entities.Count == 0)
            {
                return await Task.Run(() => false);
            }
            entities.ToList().ForEach(item =>
            {
                _context.Set<TEntity>().Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            });

            if (IsSave)
            {
                return await Task.Run(() => _context.SaveChanges() > 0);
            }
            else
            {
                return await Task.Run(() => false);
            }
        }
        #endregion

        #region Get
        public async Task<List<TEntity>> GetList()
        {
            return await Task.Run(() => _context.Set<TEntity>().AsNoTracking().ToList());
        }
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync());
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Task.Run(() => _context.Set<TEntity>().Find(id));
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _context.Set<TEntity>().AsNoTracking().SingleOrDefault(predicate));
        }
        #endregion

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, bool IsCommit = true)
        {
            var entry = _context.Set<TEntity>().Where(predicate);

            return await Task.Run(() => entry.Any());
        }

        public int? GetSum(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int?>> sum)
        {
            return _context.Set<TEntity>().Where(predicate).Sum(sum);
        }
        public Task<int> GetEntityCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().CountAsync(predicate);
        }
    }
}
