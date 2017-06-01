using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Extensions;
using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddVentureCommon()
                .BuildServiceProvider();

            var bus = (IBusClient) serviceProvider.GetService(typeof(IBusClient));

            var commandDispatcher = (ICommandDispatcher) serviceProvider.GetService(typeof(ICommandDispatcher));

            bus.SubscribeAsync<CreateProfileCommand>(
                async (command, context) =>
                {
                    Console.WriteLine(" !!! handling command !!! ");
                    await Task.Run(() => commandDispatcher.Handle(command));
                },
                config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture.Commands"));
                    config.WithRoutingKey(typeof(CreateProfileCommand).Name);
                    config.WithQueue(queue => queue.WithName("Venture.ProfileWrite"));
                });

        }
    }
}