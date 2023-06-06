using Framework.Contracts.Common;

namespace Framework.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<ITransactionService> BeginTransactionAsync();
        IEnumerable<IAggegateRoot> GetModifiedAggregateRoots();
        Task<int> SaveChange();
    }
}
