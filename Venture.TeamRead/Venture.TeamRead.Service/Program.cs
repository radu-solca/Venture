using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamRead.Application;
using Venture.TeamRead.Application.Queries;
using Venture.TeamRead.Application.QueryHandlers;
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

                .AddTransient<IQueryHandler<GetTeamCommentsQuery>, GetTeamCommentsQueryHandler>()
                .AddTransient<IQueryHandler<GetTeamUsersQuery>, GetTeamUsersQueryHandler>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var teamCreatedEventHandler = (IEventHandler<TeamCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamCreatedEvent>));
            var teamDeletedEventHandler = (IEventHandler<TeamDeletedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamDeletedEvent>));
            var teamJoinedEventHandler = (IEventHandler<TeamJoinedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamJoinedEvent>));
            var teamCommentPostedEventHandler = (IEventHandler<TeamCommentPostedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamCommentPostedEvent>));
            var teamUserApprovedEventHandler = (IEventHandler<TeamUserApprovedEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamUserApprovedEvent>));
            var teamLeftEventHandler = (IEventHandler<TeamLeftEvent>)serviceProvider.GetService(typeof(IEventHandler<TeamLeftEvent>));

            bus.SubscribeToEvent(teamCreatedEventHandler);
            bus.SubscribeToEvent(teamDeletedEventHandler);
            bus.SubscribeToEvent(teamJoinedEventHandler);
            bus.SubscribeToEvent(teamCommentPostedEventHandler);
            bus.SubscribeToEvent(teamUserApprovedEventHandler);
            bus.SubscribeToEvent(teamLeftEventHandler);

            var getTeamUsersQueryHandler = (IQueryHandler<GetTeamUsersQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetTeamUsersQuery>));
            var getTeamCommentsQueryHandler = (IQueryHandler<GetTeamCommentsQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetTeamCommentsQuery>));

            bus.SubscribeToQuery(getTeamUsersQueryHandler);
            bus.SubscribeToQuery(getTeamCommentsQueryHandler);

            Console.ReadKey();
        }
    }
}