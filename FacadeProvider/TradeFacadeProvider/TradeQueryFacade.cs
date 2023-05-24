using Domain.Contract.Trades.Repository.Query;
using Domain.Trades.Entities;
using Facade.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeProvider.TradeFacadeProvider
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

        public async Task<ITrade> GetTrade(long id)
        {
            return await tradeQuery.Get(t => t.Id == id);
        }
    }
}
