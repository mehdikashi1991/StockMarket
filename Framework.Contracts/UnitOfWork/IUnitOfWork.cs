using Framework.Contracts.Common;
using System.Transactions;

namespace Framework.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        void EnlistTransaction(CommittableTransaction transaction);
        IEnumerable<IAggegateRoot> GetModifiedAggregateRoots();
        Task OpenConnectionAsync();
        Task<int> SaveChange();
    }
}
