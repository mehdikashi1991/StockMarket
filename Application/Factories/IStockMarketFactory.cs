using Domain;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Query;

namespace Application.Factories
{
    public interface IStockMarketFactory
    {
        Task<IStockMarketMatchEngineWithState> GetStockMarket(IOrderQueryRepository orderQueryRep, ITradeQueryRespository tradeQueryRep);
    }
}