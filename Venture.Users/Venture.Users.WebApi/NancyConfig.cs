using Microsoft.EntityFrameworkCore;
using Nancy;
using Nancy.Configuration;
using Nancy.TinyIoc;
using Venture.Users.Data;

namespace Venture.Users.WebApi
{
    public class NancyConfig : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IRepository<User>, UserRepository>();
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
    }
}
