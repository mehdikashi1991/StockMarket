namespace Framework.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task OpenConnectionAsync();
        Task<int> SaveChange();
    }
}
