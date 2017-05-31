using System.Threading.Tasks;
using Venture.Gateway.Business.Events;

namespace Venture.Gateway.Business.EventDispatcher
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent command) where TEvent : class, IDomainEvent;
    }
}
