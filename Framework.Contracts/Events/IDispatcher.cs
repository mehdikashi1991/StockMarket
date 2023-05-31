using Framework.Contracts.Events;

namespace L02Application
{
    public interface IDispatcher<T> where T : IEvent
    {
        public void Dispatch(T domainEvent);
    }
}
