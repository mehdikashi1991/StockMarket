namespace Framework.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChange();
    }
}
