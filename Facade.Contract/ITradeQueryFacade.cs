using Domain.Trades.Entities;

namespace Facade.Contract
{
    public interface ITradeQueryFacade
    {
        Task<IEnumerable<ITrade>> GetAllTrades();
        Task<ITrade> GetTrade(long id);
    }
}