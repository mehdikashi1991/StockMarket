using Domain;

namespace FacadeProvider
{
    public interface ITradeQueryFacade
    {
        Task<IEnumerable<ITrade>> GetAllTrades();
        Task<ITrade> GetTrade(long id);
    }
}