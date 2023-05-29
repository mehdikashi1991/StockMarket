using Domain.Common;
using Domain.Contract.Trades.Repository.Query;
using Infrastructure.GenericServices;

namespace Infrastructure.Order.CommandRepositories
{
    public class TradeQueryRepository : QueryRepository<Domain.Trade, Domain.ITrade>,ITradeQueryRespository 
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public TradeQueryRepository(TradeMatchingEngineContext dbcontext) : base(dbcontext)
        {

        }
         
       
    }
}
