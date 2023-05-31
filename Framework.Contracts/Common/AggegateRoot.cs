using Framework.Contracts.Events;

namespace Framework.Contracts.Common
{
    public abstract class AggegateRoot : IAggegateRoot
    {
        private readonly List<IEvent> domainEvents = new();

        public abstract long Id { get; }
        public IEnumerable<IEvent> DomainEvents => domainEvents;

        public void AddDomainEvent(IEvent eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void ClearEvents()
        {
            domainEvents.Clear();
        }

        public void RemoveDomainEvent(IEvent eventItem)
        {
            domainEvents.Remove(eventItem);
        }
    }
}
