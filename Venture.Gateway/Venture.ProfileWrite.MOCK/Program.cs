using System;
using RawRabbit;
using RawRabbit.vNext;
using Venture.Common.Cqrs.Commands;
using Venture.Gateway.Business.Commands;
using Venture.Gateway.Business.Events;

// ReSharper disable InconsistentNaming

namespace Venture.ProfileWrite.MOCK
{
    class Program
    {
        private static readonly IBusClient _bus = BusClientFactory.CreateDefault();
        private static readonly CreateProfileCommandHandler _handler = new CreateProfileCommandHandler(_bus);

        static void Main(string[] args)
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!! PROFILE WRITE SERVICE MOCK !!!!!!!!!!!!!!!!!!!!!!!");

            _bus.SubscribeAsync<CreateProfileCommand>(async (msg, context) =>
            {
                _handler.ExecuteAsync(msg);
            });

            Console.Read();
        }
    }

    class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly IBusClient _bus;

        public CreateProfileCommandHandler(IBusClient bus)
        {
            _bus = bus;
        }

        public void ExecuteAsync(CreateProfileCommand command)
        {
            Console.WriteLine(" !!!!!!!!!!!!!!!!!!!!!!! Wrote profile with email " + command.Email + " !!!!!!!!!!!!!!!!!!!!!!!");

            var domainEvent = new ProfileCreatedEvent(
                command.Email,
                command.FirstName,
                command.LastName
            );

            _bus.PublishAsync(domainEvent);
        }
    }
}