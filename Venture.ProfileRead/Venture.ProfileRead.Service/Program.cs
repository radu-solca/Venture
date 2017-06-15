using System;
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
            var queryHandler = (IQueryHandler<GetProfileQuery, string>)serviceProvider.GetService(typeof(IQueryHandler<GetProfileQuery, string>));

            bus.SubscribeToEvent<ProfileCreatedEvent>("Profile", "ProfileRead", (domainEvent) => Console.WriteLine("Recieved " + domainEvent.GetType().FullName));

            bus.SubscribeToQuery(queryHandler);

            Console.ReadKey();
        }
    }
}