using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Serialization;
using RawRabbit.vNext;
using Venture.Common.Events;

namespace Venture.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVentureCommon(this IServiceCollection serviceCollection, string serviceName)
        {
            // Bus client
            BusClientExtensions.SetServiceName(serviceName);

            serviceCollection.AddRawRabbit(
                custom: ioc => ioc.AddSingleton<IMessageSerializer, CustomJsonSerializer>()
            );

            return serviceCollection;
        }

        public static IServiceCollection AddVentureEventStore(this IServiceCollection serviceCollection, string connectionString, string dbname)
        {
            serviceCollection.AddSingleton<IEventStore>(_ => new EventStore(connectionString, dbname));

            return serviceCollection;
        }
    }
}
