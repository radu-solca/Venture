using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Venture.Users.Data;

namespace Venture.Users.WebApi
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework().AddDbContext<UsersContext>(options =>
            {
                // TODO: Figure out why this doesn't work, and remove the hardcoded connection string from UsersContext.cs
                System.Console.WriteLine("This line is never reached for some reason");
                var connString = Configuration["ConnectionStrings:UserStore"];
                options.UseSqlServer(connString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseOwin(buildFunc =>
            {
                buildFunc(next => 
                    owinEnv =>
                    {
                        System.Console.WriteLine("Recieved Request");
                        return next(owinEnv);
                    }
                );
                buildFunc.UseNancy();
            });
        }
    }
}
