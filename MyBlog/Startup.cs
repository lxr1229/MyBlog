using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Data;
using MyBlog.Repository;
using MyBlog.Service;

namespace MyBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc();

            // ע��Mysql����
            var connectionString = Configuration.GetConnectionString("BlogDbContextConnection");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<BlogDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            //��������ǿ�ȣ�������Խ�������������
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.User.AllowedUserNameCharacters = null;
            });

            // ��Ӷ�AutoMapper��֧��
            services.AddAutoMapper(typeof(AutoMapperConfigs));

            // ���Identity�ķ���
            services.AddIdentity<BlogUser, IdentityRole>()
              .AddEntityFrameworkStores<BlogDbContext>()
              .AddDefaultTokenProviders();

            // ����ע�뵽DI����
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPostService,PostService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
