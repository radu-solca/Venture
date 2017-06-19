using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Serialization;
using RawRabbit.vNext;
using Venture.Common.Events;

namespace Venture.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVentureCommon(this IServiceCollection serviceCollection, string appName)
        {
            // Bus client
            BusClientExtensions.SetAppName(appName);

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
