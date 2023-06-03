namespace Framework.Contracts.Events
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T Event);
    }
}
