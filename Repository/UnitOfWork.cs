using Framework.Contracts.Common;
using Framework.Contracts.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public UnitOfWork(TradeMatchingEngineContext tradeMatchingEngineContext)
        {
            this.tradeMatchingEngineContext = tradeMatchingEngineContext;
        }

        public async ValueTask DisposeAsync()
        {
            await tradeMatchingEngineContext.DisposeAsync();
        }

        public void EnlistTransaction(CommittableTransaction transaction)
        {
            tradeMatchingEngineContext.Database.EnlistTransaction(transaction);
        }

        public IEnumerable<IAggegateRoot> GetModifiedAggregateRoots()
        {
            return tradeMatchingEngineContext.ChangeTracker.Entries<IAggegateRoot>()
                .Select(x => x.Entity).ToArray();
        }

        public async Task OpenConnectionAsync()
        {
            await tradeMatchingEngineContext.Database.OpenConnectionAsync().ConfigureAwait(false);
        }

        public async Task<int> SaveChange()
        {
            var result = await tradeMatchingEngineContext.SaveChangesAsync();
            return result;
        }
    }
}
