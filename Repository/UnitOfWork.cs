using Framework.Contracts.UnitOfWork;
using Microsoft.EntityFrameworkCore;

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
