using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamWrite.Application;
using Venture.TeamWrite.Application.EventHandlers;
using Venture.TeamWrite.Domain;
using Venture.TeamWrite.Domain.DomainEvents;

namespace Venture.TeamWrite.Service
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("TeamWrite");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("TeamWrite")
                .AddVentureEventStore("localhost", "TeamWrite")
                .AddTransient<IRepository<Team>, TeamRepository>()

                .AddTransient<IEventHandler<ProjectCreatedEvent>, ProjectCreatedEventHandler>()
                .AddTransient<IEventHandler<ProjectDeletedEvent>, ProjectDeletedEventHandler>()


                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));
            var projectCreatedEventHandler = (IEventHandler<ProjectCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCreatedEvent>));
            var projectDeletedEventHandler = (IEventHandler<ProjectDeletedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectDeletedEvent>));

            bus.SubscribeToEvent(projectCreatedEventHandler);
            bus.SubscribeToEvent(projectDeletedEventHandler);

            Console.ReadKey();
        }
    }
}