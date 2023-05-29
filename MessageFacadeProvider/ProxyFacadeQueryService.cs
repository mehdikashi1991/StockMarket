using Domain.Orders.Entities;
using Facade.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFacadeProvider
{
    public class ProxyOrderFacadeQueryService : IOrderQueryFacade
    {
        public Task<IOrder> Get(long id)
        {
            throw new NotImplementedException();
        }
    }
}
