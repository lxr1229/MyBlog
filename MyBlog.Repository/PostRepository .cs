using MyBlog.Data;

namespace MyBlog.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
