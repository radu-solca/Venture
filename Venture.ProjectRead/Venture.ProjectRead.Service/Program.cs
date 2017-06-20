using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.ProjectRead.Application;
using Venture.ProjectRead.Application.DomainEvents;

namespace Venture.ProjectRead.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProjectRead");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProjectWrite")
                .AddVentureEventStore("localhost", "ProjectWrite")
                .AddTransient<IEventHandler<ProjectCreatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectTitleUpdatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectDescriptionUpdatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectTagsUpdatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectCommentPostedEvent>, ProjectDenormalizer>()
                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var projectCreatedEventHandler = (IEventHandler<ProjectCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCreatedEvent>));
            var projectTitleUpdatedEvent = (IEventHandler<ProjectTitleUpdatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectTitleUpdatedEvent>));
            var projectDescriptionUpdatedEvent = (IEventHandler<ProjectDescriptionUpdatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectDescriptionUpdatedEvent>));
            var projectTagsUpdatedEvent = (IEventHandler<ProjectTagsUpdatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectTagsUpdatedEvent>));
            var projectCommentPostedEvent = (IEventHandler<ProjectCommentPostedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCommentPostedEvent>));

            bus.SubscribeToEvent(projectCreatedEventHandler);
            bus.SubscribeToEvent(projectTitleUpdatedEvent);
            bus.SubscribeToEvent(projectDescriptionUpdatedEvent);
            bus.SubscribeToEvent(projectTagsUpdatedEvent);
            bus.SubscribeToEvent(projectCommentPostedEvent);

            Console.ReadKey();
        }
    }
}