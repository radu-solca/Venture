using System;
using LiteGuard;
using Venture.Gateway.Business.EventHandlers;
using Venture.Gateway.Business.Events;

namespace Venture.Gateway.Business.EventDispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            Guard.AgainstNullArgument(nameof(serviceProvider), serviceProvider);
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TEvent>(TEvent command) where TEvent : class, IDomainEvent
        {
            Guard.AgainstNullArgument(nameof(command), command);

            IEventHandler<TEvent> handler = (IEventHandler<TEvent>)_serviceProvider.GetService(typeof(IEventHandler<TEvent>));

            if (handler == null)
            {
                throw new Exception("Event handler not found for type " + typeof(IEventHandler<TEvent>));
            }

            handler.Execute(command);
        }
    }
}
