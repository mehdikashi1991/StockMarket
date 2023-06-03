using Framework.Contracts.Events;

namespace L02Application
{
    public interface IDispatcher
    {
        public void Dispatch<T>(T domainEvent) where T : class, IDomainEvent;
    }
}
