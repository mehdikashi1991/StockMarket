using Infrastructure.GenericServices;
using Microsoft.EntityFrameworkCore;
using Domain.Contract.Trades.Repository.Query;
using Domain.Trades.Entities;

namespace Infrastructure.Trades.QueryRepositories
{
    public class TradeQueryRepository : QueryRepository<Trade, ITrade>, ITradeQueryRespository
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public TradeQueryRepository(TradeMatchingEngineContext dbcontext) : base(dbcontext)
        {

        }


    }
}
