namespace Framework.Contracts.Common
{
    public interface IBaseEntity<T>
    {
        public T Id { get; }
    }
}
