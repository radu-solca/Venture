using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Extensions;
using Venture.ProfileRead.Business.Events;
using Venture.ProfileRead.Business.Queries;
using Venture.ProfileRead.Business.QueryHandlers;

namespace Venture.ProfileRead.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProfileRead started.");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon()
                .AddTransient<IQueryHandler<GetProfileQuery, string>, GetProfileQueryHandler>()
                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));
            var queryDispatcher = (IQueryDispatcher)serviceProvider.GetService(typeof(IQueryDispatcher));

            bus.SubscribeAsync<ProfileCreatedEvent>(
                async (domainEvent, context) =>
                {
                    await Task.Run(() => Console.WriteLine(domainEvent.Type + " recieved"));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture.Profile.Events"));
                    config.WithRoutingKey("Profile.Event");
                    config.WithQueue(queue => queue.WithName("Venture.ProfileRead"));
                });

            bus.RespondAsync<GetProfileQuery, string>(
                async (query, context) =>
                {
                    return await Task.Run(() => queryDispatcher.Handle(query));
                },
                 config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture.Queries"));
                    config.WithRoutingKey(typeof(GetProfileQuery).Name);
                });

            Console.ReadKey();
        }
    }
}