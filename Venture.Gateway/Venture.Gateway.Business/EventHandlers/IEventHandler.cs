using System.Threading.Tasks;
using Venture.Gateway.Business.Events;

namespace Venture.Gateway.Business.EventHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task ExecuteAsync(TEvent domainEvent);
    }
}
