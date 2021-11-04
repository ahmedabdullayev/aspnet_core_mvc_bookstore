using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Helpers;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>(); // signup user etc
            services.Configure<IdentityOptions>(options => // lambda expression
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
                 // TO REMOVE CLIENt SIDE VALIDATIONS ↓
                // .AddViewOptions(option => option.HtmlHelperOptions.ClientValidationEnabled = false);
            services.AddScoped<IBookRepository, BookRepository>(); //dependency inj
            services.AddScoped<ILanguageRepository, LanguageRepository>(); //dependency inj
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
            });

            // Relationship for model and appsettings:SMTPConfig section,FOR EMAIL SEND
            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig")); 

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>(); // Helper/Claims
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //middlewares
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "testImages")),
                RequestPath = "/testImages"
            });
            app.UseRouting();

            app.UseAuthentication(); // signup
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                // endpoints.MapControllerRoute(
                //     name: "Default",
                //     pattern: "bookApp/{controller=Home}/{action=Index}/{id?}"
                // );
                
                // endpoints.MapControllerRoute(
                //     name: "AboutUs",
                //     pattern: "about-us/{id?}",
                //     defaults: new {controller = "Home", action = "AboutUs"}
                // );
            });
        }
    }
}