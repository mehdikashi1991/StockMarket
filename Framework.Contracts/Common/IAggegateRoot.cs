using Framework.Contracts.Events;

namespace Framework.Contracts.Common
{
    public interface IAggegateRoot
    {
        public long Id { get; }
        IEnumerable<IEvent> DomainEvents { get; }

        void AddDomainEvent(IEvent eventItem);
        void ClearEvents();
        void RemoveDomainEvent(IEvent eventItem);
    }
}
