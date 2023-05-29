using Domain.Trades.Entities;
using Framework.Contracts.Common;

namespace Facade.Contract
{
    public interface ITradeQueryFacade
    {
        Task<IEnumerable<ITrade>> GetAllTrades();
        Task<ITrade> GetTrade(long id);
        Task<PageResult<Trade>> GetAllTradesWithPaging(int page, int pageSize, int currentPage, long lastId);
    }
}