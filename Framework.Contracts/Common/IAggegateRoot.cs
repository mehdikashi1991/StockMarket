using Framework.Contracts.Events;

namespace Framework.Contracts.Common
{
    public interface IAggegateRoot
    {
        public long Id { get; }
        IEnumerable<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent eventItem);
        void ClearEvents();
        void RemoveDomainEvent(IDomainEvent eventItem);
    }
}
