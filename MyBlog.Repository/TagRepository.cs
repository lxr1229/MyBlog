using MyBlog.Data;

namespace MyBlog.Repository
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        private readonly BlogDbContext _context;

        public TagRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
