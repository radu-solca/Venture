using Microsoft.Extensions.DependencyInjection;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;

namespace Venture.Common.Cqrs.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddVentureCqrs(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<ICommandDispatcher, CommandDispatcher>();
            serviceProvider.AddTransient<IQueryDispatcher, QueryDispatcher>();
            return serviceProvider;
        }
    }
}
