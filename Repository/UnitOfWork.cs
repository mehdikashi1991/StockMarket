using Framework.Contracts;
using Framework.Contracts.Common;
using Framework.Contracts.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;
        private readonly ITransactionService transactionService;
        private readonly DbConnectionManager? tran;

        public UnitOfWork(TradeMatchingEngineContext tradeMatchingEngineContext,
            ITransactionService transactionService)
        {
            this.tradeMatchingEngineContext = tradeMatchingEngineContext;
            this.transactionService = transactionService;
            tran = transactionService as DbConnectionManager;
            Debug.Assert(tran != null, $"In {this.GetType().Name} class when casting {typeof(ITransactionService).Name} to {typeof(DbConnectionManager).Name} it resulted to null");
        }

        public async Task<ITransactionService> BeginTransactionAsync()
        {
            await tran.BeginTransactionAsync().ConfigureAwait(false);
            tradeMatchingEngineContext.Database.UseTransaction(tran.Transaction);
            return transactionService;
        }

        public void Dispose()
        {
            this.DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async ValueTask DisposeAsync()
        {
            await tradeMatchingEngineContext.DisposeAsync().ConfigureAwait(false);
        }

        public IEnumerable<IAggegateRoot> GetModifiedAggregateRoots()
        {
            return tradeMatchingEngineContext.ChangeTracker.Entries<IAggegateRoot>()
                .Select(x => x.Entity).ToArray();
        }

        public async Task<int> SaveChange()
        {
            var result = await tradeMatchingEngineContext.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }
    }
}
