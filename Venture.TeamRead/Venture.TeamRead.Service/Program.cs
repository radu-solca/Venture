using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamRead.Application;
using Venture.TeamRead.Data;
using Venture.TeamRead.Data.DomainEvents;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Service
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("TeamRead");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("TeamRead")

                .AddDbContext<TeamReadContext>(options =>
                    options.UseSqlServer(connectionString)
                )

                // Repositories
                .AddTransient<IRepository<Team>, TeamRepository>()
                .AddTransient<IRepository<TeamMembership>, TeamMembershipRepository>()
                .AddTransient<IRepository<Comment>, CommentRepository>()
                .AddTransient<CommentRepository>()

                .AddTransient<IEventHandler<TeamCreatedEvent>, TeamDenormalizer>()
                .AddTransient<IEventHandler<TeamDeletedEvent>, TeamDenormalizer>()
                .AddTransient<IEventHandler<TeamJoinedEvent>, TeamDenormalizer>()
                .AddTransient<IEventHandler<TeamCommentPostedEvent>, TeamDenormalizer>()
                .AddTransient<IEventHandler<TeamUserApprovedEvent>, TeamDenormalizer>()
                .AddTransient<IEventHandler<TeamLeftEvent>, TeamDenormalizer>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var teamCreatedEvent = (IEventHandler<TeamCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamCreatedEvent>));
            var teamDeletedEvent = (IEventHandler<TeamDeletedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamDeletedEvent>));
            var teamJoinedEvent = (IEventHandler<TeamJoinedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamJoinedEvent>));
            var teamCommentPostedEvent = (IEventHandler<TeamCommentPostedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamCommentPostedEvent>));
            var teamUserApprovedEvent = (IEventHandler<TeamUserApprovedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamUserApprovedEvent>));
            var teamLeftEvent = (IEventHandler<TeamLeftEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamLeftEvent>));

            bus.SubscribeToEvent(teamCreatedEvent);
            bus.SubscribeToEvent(teamDeletedEvent);
            bus.SubscribeToEvent(teamJoinedEvent);
            bus.SubscribeToEvent(teamCommentPostedEvent);
            bus.SubscribeToEvent(teamUserApprovedEvent);
            bus.SubscribeToEvent(teamLeftEvent);

            Console.ReadKey();
        }
    }
}