namespace Venture.Common.Events
{
    public interface IEventHandler<TEvent>
        where TEvent : DomainEvent
    {
        void Handle(TEvent domainEvent);
    }
}
