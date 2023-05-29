using Domain.Trades.Entities;
using Facade.Contract;
using Framework.Contracts.Common;

namespace MessageFacadeProvider
{
    public class ProxyTradeFacadeQueryService : ITradeQueryFacade
    { 
        public Task<IEnumerable<ITrade>> GetAllTrades()
        {
            throw new NotImplementedException();
        }

        public Task<PageResult<Trade>> GetAllTradesWithPaging(int page, int pageSize, int currentPage, long lastId)
        {
            throw new NotImplementedException();
        }

        public Task<ITrade> GetTrade(long id)
        {
            throw new NotImplementedException();
        }
    }
}
