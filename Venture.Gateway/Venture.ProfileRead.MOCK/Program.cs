using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Events;
using Venture.Common.Extensions;

namespace Venture.ProfileRead.MOCK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProfileRead started.");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon()
                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            bus.SubscribeAsync<DomainEvent>(
                async (domainEvent, context) =>
                {
                    await Task.Run(() => Console.WriteLine(domainEvent.Type + " recieved"));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture.Events"));
                    config.WithRoutingKey("profileEvent");
                    config.WithQueue(queue => queue.WithName("Venture.ProfileRead"));
                });

            Console.ReadKey();
        }
    }
}