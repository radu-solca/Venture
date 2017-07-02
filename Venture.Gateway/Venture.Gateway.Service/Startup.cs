using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Venture.Common.Extensions;

namespace Venture.Gateway.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVentureCommon("Gateway");
            services.AddCors(o => o.AddPolicy("OpenBordersPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //var tokenValidationParameters = new TokenValidationParameters

            //{
            //    ValidateIssuerSigningKey = true,
            //    ValidateIssuer = true,
            //    ValidIssuer = "http://localhost:5000/",
            //    IssuerSigningKey = new X509SecurityKey(new X509Certificate2(certLocation)),
            //};

            app.UseCors("OpenBordersPolicy");
            //app.UseJwtBearerAuthentication(
            //    new JwtBearerOptions()
            //    {
            //        AutomaticAuthenticate = true,
            //        RequireHttpsMetadata = false,
            //        TokenValidationParameters = tokenValidationParameters
            //    });
            app.UseMvc();
        }
    }
}
