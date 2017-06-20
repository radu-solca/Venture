using System;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Extensions;
using Venture.ProfileWrite.Business.Commands;
using Venture.ProfileWrite.Business.CommandHandlers;

namespace Venture.ProfileWrite.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ProfileWrite started.");

            var serviceProvider = new ServiceCollection()
                .AddVentureCommon("ProfileWrite")
                .AddVentureEventStore("localhost", "ProfileWrite")
                .AddTransient<ICommandHandler<CreateProfileCommand>, CreateProfileComandHandler>()
                .BuildServiceProvider();

            var bus = (IBusClient) serviceProvider.GetService(typeof(IBusClient));

            var commandHandler = (ICommandHandler<CreateProfileCommand>) serviceProvider.GetService(typeof(ICommandHandler<CreateProfileCommand>));

            bus.SubscribeToCommand(commandHandler);
        }
    }
}