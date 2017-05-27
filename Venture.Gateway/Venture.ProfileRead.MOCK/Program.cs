using System;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.vNext;
using Venture.Gateway.Business.EventHandlers;
using Venture.Gateway.Business.Events;
using Venture.Gateway.Business.Extensions;
// ReSharper disable InconsistentNaming

namespace Venture.ProfileRead.MOCK
{
    class Program
    {
        private static readonly IBusClient _bus = BusClientFactory.CreateDefault();
        private static readonly ProfileCreatedEventHandler _handler = new ProfileCreatedEventHandler();

        static void Main(string[] args)
        {
            Console.WriteLine("PROFILE READ SERVICE MOCK");

            _bus.SubscribeToEvent(_handler);

            Console.Read();
        }
    }

    class ProfileCreatedEventHandler : IEventHandler<ProfileCreatedEvent>
    {
        public async Task ExecuteAsync(ProfileCreatedEvent domainEvent)
        {
            Console.WriteLine("Updated view model for new profile with email " + domainEvent.Email);
        }
    }
}