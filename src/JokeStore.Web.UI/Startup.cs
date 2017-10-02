using JokeStore.Core.Repository;
using JokeStore.Core.Repository.EntityFramework;
using JokeStore.Web.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeStore.Web.UI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite();
            services.AddMvc();
            services.AddDbContext<DataContext>(options => options.UseSqlite("Filename=JokeStore.db"));

            services.AddSingleton<IDomainResolver>(new RequestDomainResolver("localhost"));
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IDomainRepository, Repository>();
            services.AddTransient<IEntryRepository, Repository>();

            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.Use((context, next) =>
                {
                    if (env.IsDevelopment())
                    {
                        DataContext db = context.RequestServices.GetService<DataContext>();
                        if (db.Database.EnsureCreated())
                            DataContextInitializer.Seed(db);
                    }

                    return next.Invoke();
                });
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(null,
                    "",
                    new { controller = "Entry", action = "List", category = (string)null, page = 1 }
                );

                routes.MapRoute(null,
                    "page{page}",
                    new { controller = "Entry", action = "List", category = (string)null },
                    new { page = @"\d+" } // Constraints: page must be numerical
                );

                routes.MapRoute(null,
                    "{category}",
                    new { controller = "Entry", action = "List", page = 1 }
                );

                routes.MapRoute(null,
                    "{category}/page{page}",
                    new { controller = "Entry", action = "List" },
                    new { page = @"\d+" }
                );

                routes.MapRoute(null,
                    "vote/{entryID}/{direction}",
                    new { controller = "Entry", action = "Vote" },
                    new { entryID = @"\d+" }
                );

                routes.MapRoute(null, "{controller}/{action}");
            });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Not Found!");
            });
        }
    }

    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            string[] locations = new string[] { "/Views/{2}/{1}/{0}.cshtml" };
            return locations.Union(viewLocations);
        }


        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
        }
    }
}
