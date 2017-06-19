using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.Common.Extensions;
using Venture.ProjectWrite.Application;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProjectWrite");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProjectWrite")
                .AddVentureEventStore("localhost", "ProjectWrite")
                .AddTransient<Repository<Project>, ProjectRepository>()
                .AddTransient<ICommandHandler<CreateProjectCommand>, CreateProjectCommandHandler>()
                .AddTransient<ICommandHandler<UpdateProjectCommand>, UpdateProjectCommandHandler>()
                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));
            var createProjectCommandHandler = (ICommandHandler<CreateProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<CreateProjectCommand>));
            var updateProjectCommandHandler = (ICommandHandler<UpdateProjectCommand>)serviceProvider.GetService(typeof(ICommandHandler<UpdateProjectCommand>));

            bus.SubscribeToCommand(createProjectCommandHandler);
            bus.SubscribeToCommand(updateProjectCommandHandler);

            Console.ReadKey();
        }
    }
}