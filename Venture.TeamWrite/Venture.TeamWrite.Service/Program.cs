using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamWrite.Application;
using Venture.TeamWrite.Application.CommandHandlers;
using Venture.TeamWrite.Application.Commands;
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

                .AddTransient<ICommandHandler<ApproveTeamUserCommand>, ApproveTeamUserCommandHandler>()
                .AddTransient<ICommandHandler<JoinTeamCommand>, JoinTeamCommandHandler>()
                .AddTransient<ICommandHandler<LeaveTeamCommand>, LeaveTeamCommandHandler>()
                .AddTransient<ICommandHandler<PostCommentOnTeamCommand>, PostCommentOnTeamCommandHandler>()

                .BuildServiceProvider();

            var bus = (IBusClient)serviceProvider.GetService(typeof(IBusClient));

            var projectCreatedEventHandler = (IEventHandler<ProjectCreatedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectCreatedEvent>));
            var projectDeletedEventHandler = (IEventHandler<ProjectDeletedEvent>)serviceProvider.GetService(typeof(IEventHandler<ProjectDeletedEvent>));

            bus.SubscribeToEvent(projectCreatedEventHandler);
            bus.SubscribeToEvent(projectDeletedEventHandler);

            var approveTeamUserCommandHandler = (ICommandHandler<ApproveTeamUserCommand>)serviceProvider.GetService(typeof(ICommandHandler<ApproveTeamUserCommand>));
            var joinTeamCommandHandler = (ICommandHandler<JoinTeamCommand>)serviceProvider.GetService(typeof(ICommandHandler<JoinTeamCommand>));
            var leaveTeamCommandHandler = (ICommandHandler<LeaveTeamCommand>)serviceProvider.GetService(typeof(ICommandHandler<LeaveTeamCommand>));
            var postCommentOnTeamCommandHandler = (ICommandHandler<PostCommentOnTeamCommand>)serviceProvider.GetService(typeof(ICommandHandler<PostCommentOnTeamCommand>));

            bus.SubscribeToCommand(approveTeamUserCommandHandler);
            bus.SubscribeToCommand(joinTeamCommandHandler);
            bus.SubscribeToCommand(leaveTeamCommandHandler);
            bus.SubscribeToCommand(postCommentOnTeamCommandHandler);

            Console.ReadKey();
        }
    }
}