using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MVCEntityMSSQLSignalR.BLL.Services;
using MVCEntityMSSQLSignalR.DAL.Contexts;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using MVCEntityMSSQLSignalR.DAL.Repositories;
using MVCEntityMSSQLSignalR.SignalR;
using System.IO;

namespace MVCEntityMSSQLSignalR
{
    public class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration">Configuration object</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration get-property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Service configuration method
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string filesConnection = Configuration.GetConnectionString("FilesConnection");
            services.AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(connection));
            services.AddDbContext<FileContext>(options =>
                options.UseSqlServer(filesConnection));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                            {
                                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                            });
            services.AddControllersWithViews();
            services.AddSignalR();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IBotService, BotService>();
            services.AddTransient<IRepository<DAL.Entities.User>, UserRepository>();
            services.AddTransient<IRepository<DAL.Entities.Message>, MessageRepository>();
            services.AddTransient<IRepository<DAL.Entities.File>, FileRepository>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
        }

        /// <summary>
        /// Configuration method
        /// </summary>
        /// <param name="app">Application builder object</param>
        /// <param name="env">Host environment object</param>
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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\music")),
                RequestPath = new PathString("/music")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
