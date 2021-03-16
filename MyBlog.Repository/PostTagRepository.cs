using MyBlog.Data;

namespace MyBlog.Repository
{
    public class PostTagRepository : BaseRepository<PostTag>, IPostTagRepository
    {
        private readonly BlogDbContext _context;

        public PostTagRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
