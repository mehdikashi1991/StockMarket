using Domain;
using Domain.Common;
using Domain.Contract.Trades.Repository.Query;

namespace FacadeProvider
{
    public class TradeQueryFacade : ITradeQueryFacade
    {
        private readonly ITradeQueryRespository tradeQuery;

        public TradeQueryFacade(ITradeQueryRespository tradeQuery)
        {
            this.tradeQuery = tradeQuery;
        }
        public async Task<IEnumerable<ITrade>> GetAllTrades()
        {
            return await tradeQuery.GetAll();
        }

        public async Task<PageResult<Trade>> GetAllTradesWithPaging(int page, int pageSize, int currentPage, long lastId)
        {
            return await tradeQuery.GetPaging(page, pageSize, currentPage, lastId);
        }

        public async Task<ITrade> GetTrade(long id)
        {
            return await tradeQuery.Get(t => t.Id == id);
        }
    }
}
