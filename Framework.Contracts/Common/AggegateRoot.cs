using Framework.Contracts.Events;

namespace Framework.Contracts.Common
{
    public abstract class AggegateRoot : IAggegateRoot
    {
        private readonly List<IDomainEvent> domainEvents = new();

        public abstract long Id { get; }
        public IEnumerable<IDomainEvent> DomainEvents => domainEvents;

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void ClearEvents()
        {
            domainEvents.Clear();
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            domainEvents.Remove(eventItem);
        }
    }
}
