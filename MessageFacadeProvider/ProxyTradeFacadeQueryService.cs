using Domain.Orders.Entities;
using Domain.Trades.Entities;
using Facade.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFacadeProvider
{
    public class ProxyTradeFacadeQueryService : ITradeQueryFacade
    { 
        public Task<IEnumerable<ITrade>> GetAllTrades()
        {
            throw new NotImplementedException();
        }

        public Task<ITrade> GetTrade(long id)
        {
            throw new NotImplementedException();
        }
    }
}
