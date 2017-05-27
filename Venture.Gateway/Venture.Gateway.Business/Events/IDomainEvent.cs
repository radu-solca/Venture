namespace Venture.Gateway.Business.Events
{
    public interface IDomainEvent
    {
        long SequenceNumber { get; }
    }
}
