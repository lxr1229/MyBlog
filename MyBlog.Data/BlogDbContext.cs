using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Data
{
    public class BlogDbContext : IdentityDbContext<BlogUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
