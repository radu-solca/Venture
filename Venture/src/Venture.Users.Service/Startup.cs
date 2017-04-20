using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace Venture.Users.Service
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(
                buildFunc => buildFunc.UseNancy()
            );
        }
    }
}
