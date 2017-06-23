using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.Common.Extensions;
using Venture.ProjectWrite.Application;
using Venture.ProjectWrite.Application.CommandHandlers;
using Venture.ProjectWrite.Application.Commands;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Service
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ProjectWrite");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProjectWrite")
                .AddVentureEventStore("localhost", "ProjectWrite")
                .AddTransient<IRepository<Project>, ProjectRepository>()

                .AddTransient<ICommandHandler<CreateProjectCommand>, CreateProjectCommandHandler>()
                .AddTransient<ICommandHandler<UpdateProjectCommand>, UpdateProjectCommandHandler>()
                .AddTransient<ICommandHandler<PostCommentOnProjectCommand>, PostCommentOnProjectCommandHandler>()
                .AddTransient<ICommandHandler<DeleteProjectCommand>, DeleteProjectCommandHandler>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));
            var createProjectCommandHandler = (ICommandHandler<CreateProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<CreateProjectCommand>));
            var updateProjectCommandHandler = (ICommandHandler<UpdateProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<UpdateProjectCommand>));
            var postCommentOnProjectCommandHandler = (ICommandHandler<PostCommentOnProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<PostCommentOnProjectCommand>));
            var deleteProjectCommandHandler = (ICommandHandler<DeleteProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<DeleteProjectCommand>));

            bus.SubscribeToCommand(createProjectCommandHandler);
            bus.SubscribeToCommand(updateProjectCommandHandler);
            bus.SubscribeToCommand(postCommentOnProjectCommandHandler);
            bus.SubscribeToCommand(deleteProjectCommandHandler);

            Console.ReadKey();
        }
    }
}