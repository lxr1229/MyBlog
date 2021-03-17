using MyBlog.Data;
using System.Threading.Tasks;

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
