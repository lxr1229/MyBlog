using MyBlog.Data;

namespace MyBlog.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly BlogDbContext _context;

        public CategoryRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
