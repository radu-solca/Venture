using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.ProjectRead.Application;
using Venture.ProjectRead.Data;
using Venture.ProjectRead.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Venture.Common.Cqrs.Queries;
using Venture.ProjectRead.Application.Queries;
using Venture.ProjectRead.Application.QueryHandlers;
using Venture.ProjectRead.Data.DomainEvents;

namespace Venture.ProjectRead.Service
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ProjectRead");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProjectRead")

                .AddDbContext<ProjectReadContext>(options =>
                    options.UseSqlServer(connectionString)
                )

                // Repositories
                .AddTransient<IRepository<Project>, ProjectRepository>()
                .AddTransient<IRepository<Comment> ,CommentRepository >()
                .AddTransient<CommentRepository>()

                // Event handlers
                .AddTransient<IEventHandler<ProjectCreatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectTitleUpdatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectDescriptionUpdatedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectCommentPostedEvent>, ProjectDenormalizer>()
                .AddTransient<IEventHandler<ProjectDeletedEvent>, ProjectDenormalizer>()

                // Query handlers
                .AddTransient<IQueryHandler<GetProjectQuery>, GetProjectQueryHandler>()
                .AddTransient<IQueryHandler<GetProjectsQuery>, GetProjectsQueryHandler>()
                .AddTransient<IQueryHandler<GetProjectCommentsQuery>, GetProjectCommentsQueryHandler>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var projectCreatedEventHandler = (IEventHandler<ProjectCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCreatedEvent>));
            var projectTitleUpdatedEventHandler = (IEventHandler<ProjectTitleUpdatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectTitleUpdatedEvent>));
            var projectDescriptionUpdatedEventHandler = (IEventHandler<ProjectDescriptionUpdatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectDescriptionUpdatedEvent>));
            var projectCommentPostedEventHandler = (IEventHandler<ProjectCommentPostedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCommentPostedEvent>));
            var projectDeletedEventHandler = (IEventHandler<ProjectDeletedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectDeletedEvent>));

            bus.SubscribeToEvent(projectCreatedEventHandler);
            bus.SubscribeToEvent(projectTitleUpdatedEventHandler);
            bus.SubscribeToEvent(projectDescriptionUpdatedEventHandler);
            bus.SubscribeToEvent(projectCommentPostedEventHandler);
            bus.SubscribeToEvent(projectDeletedEventHandler);

            var getProjectQueryHandler = (IQueryHandler<GetProjectQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetProjectQuery>));
            var getProjectsQueryHandler = (IQueryHandler<GetProjectsQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetProjectsQuery>));
            var getProjectCommentsQueryHandler = (IQueryHandler<GetProjectCommentsQuery>)serviceProvider.GetService(typeof(IQueryHandler<GetProjectCommentsQuery>));

            bus.SubscribeToQuery(getProjectQueryHandler);
            bus.SubscribeToQuery(getProjectsQueryHandler);
            bus.SubscribeToQuery(getProjectCommentsQueryHandler);

            Console.ReadKey();
        }
    }
}