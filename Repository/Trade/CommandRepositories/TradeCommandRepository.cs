using Domain.Contract.Trades.Repository.Command;
using Infrastructure.Order.QueryRepositories;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Trade.QueryRepositories
{
    public class TradeCommandRepository : CommandRepository<Domain.Trade, Domain.ITrade>,ITradeCommandRepository
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public TradeCommandRepository(TradeMatchingEngineContext dbcontext) : base(dbcontext)
        {
        }
    }
}
