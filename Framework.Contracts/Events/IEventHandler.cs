namespace Framework.Contracts.Events
{
    public interface IEventHandler<T> where T : IEvent
    {
        void Handle(T Event);
    }
}
