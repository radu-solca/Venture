using System;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.vNext;
using Venture.Common.Cqrs.Queries;
using Venture.Gateway.Business.EventHandlers;
using Venture.Gateway.Business.Events;
using Venture.Gateway.Business.Queries;

// ReSharper disable InconsistentNaming

namespace Venture.ProfileRead.MOCK
{
    class Program
    {
        private static readonly IBusClient _bus = BusClientFactory.CreateDefault();
        private static readonly ProfileCreatedEventHandler _handler = new ProfileCreatedEventHandler();
        private static readonly GetProfileQueryHandler _queryHandler = new GetProfileQueryHandler();

        static void Main(string[] args)
        {
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!! PROFILE READ SERVICE MOCK !!!!!!!!!!!!!!!!!!!!!!!");

            System.Threading.Thread.Sleep(30000);

            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!! PROFILE SUBSCRIBED TO EVENT !!!!!!!!!!!!!!!!!!!!!!!");

            _bus.SubscribeAsync<ProfileCreatedEvent>(async (domainEvent, context) =>
            {
                await _handler.ExecuteAsync(domainEvent);
            });

            _bus.RespondAsync<GetProfileQuery, string>(async (query, context) =>
            {
                return _queryHandler.Retrieve(query);
            });

            Console.Read();
        }
    }

    class ProfileCreatedEventHandler : IEventHandler<ProfileCreatedEvent>
    {
        public async Task ExecuteAsync(ProfileCreatedEvent domainEvent)
        {
            Console.WriteLine(" !!!!!!!!!!!!!!!!!!!!!!! Updated view model for new profile with email " + domainEvent.Email + "!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, string>
    {
        public string Retrieve(GetProfileQuery query)
        {
            Console.WriteLine(" !!!!!!!!!!!!!!!!!!!!!!! Got request for profiles. !!!!!!!!!!!!!!!!!!!!!!! ");
            return "THis is a profile";
        }
    }
}