namespace Domain.Common
{
    public interface IBaseEntity<T>
    {
        public T Id { get; }   
    }
}
