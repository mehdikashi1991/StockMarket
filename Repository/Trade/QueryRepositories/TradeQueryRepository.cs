using Infrastructure.GenericServices;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Contract.Trades.Repository.Query;

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
