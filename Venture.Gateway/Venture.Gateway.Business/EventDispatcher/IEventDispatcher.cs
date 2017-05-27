using Venture.Gateway.Business.Events;

namespace Venture.Gateway.Business.EventDispatcher
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent command) where TEvent : class, IDomainEvent;
    }
}
