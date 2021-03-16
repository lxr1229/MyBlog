using MyBlog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public BlogDbContext _context;
        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
