using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(
                @" Server=barrel.itcollege.ee;User Id=student;Password=Student.Pass.1;Database=student_ahabdu_bookstore;MultipleActiveResultSets=true; "));
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
                 // TO REMOVE CLIENt SIDE VALIDATIONS ↓
                // .AddViewOptions(option => option.HtmlHelperOptions.ClientValidationEnabled = false);
            services.AddScoped<BookRepository, BookRepository>(); //dependency
            services.AddScoped<LanguageRepository, LanguageRepository>(); //dependency
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
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                // endpoints.MapControllerRoute(
                //     name: "Default",
                //     pattern: "bookApp/{controller=Home}/{action=Index}/{id?}"
                // );
                endpoints.MapControllerRoute(
                    name: "AboutUs",
                    pattern: "about-us/{id?}",
                    defaults: new {controller = "Home", action = "AboutUs"}
                );
            });
        }
    }
}