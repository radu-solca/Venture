using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Extensions;
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

            bus.SubscribeToEvent("Profile", "ProfileRead", (domainEvent) =>
            {
                Console.WriteLine("Recieved " + domainEvent.GetType().FullName);
                Console.WriteLine(domainEvent.Type);
                Console.WriteLine(domainEvent.AggregateId);
                Console.WriteLine(domainEvent.Id);
                Console.WriteLine(domainEvent.OccuredAt);
                Console.WriteLine(domainEvent.Version);

                var payload = domainEvent.JsonPayload;

                Console.WriteLine(payload);
            });

            bus.SubscribeToQuery(queryHandler);

            Console.ReadKey();
        }
    }

    class CreateProfilePayload
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}