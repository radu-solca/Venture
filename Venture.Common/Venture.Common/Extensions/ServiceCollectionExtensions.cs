using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Serialization;
using RawRabbit.vNext;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Events;

namespace Venture.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVentureCommon(this IServiceCollection serviceCollection)
        {
            // Cqrs dispatchers
            serviceCollection.AddTransient<ICommandDispatcher, CommandDispatcher>();
            serviceCollection.AddTransient<IQueryDispatcher, QueryDispatcher>();

            // Bus client
            serviceCollection.AddRawRabbit(
                custom: ioc => ioc.AddSingleton<IMessageSerializer, CustomJsonSerializer>()
            );

            return serviceCollection;
        }

        public static IServiceCollection AddVentureEventStore(this IServiceCollection serviceCollection, string connectionString, string dbname)
        {
            serviceCollection.AddSingleton<IEventStore>(provider =>
            {
                var bus = provider.GetService<IBusClient>();

                return new EventStore(bus, connectionString, dbname);
            });

            return serviceCollection;
        }
    }
}
