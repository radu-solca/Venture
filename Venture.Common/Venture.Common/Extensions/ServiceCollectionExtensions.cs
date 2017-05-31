using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.vNext;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;

namespace Venture.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVentureCommon(this IServiceCollection serviceProvider)
        {
            // Cqrs dispatchers
            serviceProvider.AddTransient<ICommandDispatcher, CommandDispatcher>();
            serviceProvider.AddTransient<IQueryDispatcher, QueryDispatcher>();

            // Bus client
            Func<IServiceProvider, IBusClient> busClientSupplier = p => BusClientFactory.CreateDefault();
            serviceProvider.AddSingleton(busClientSupplier);

            return serviceProvider;
        }
    }
}
