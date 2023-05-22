using Domain;
using Domain.Contract.Orders.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeProvider
{
    public class OrderQueryFacade : IOrderQueryFacade
    {
        private readonly IOrderQueryRepository orderQuery;

        public OrderQueryFacade(IOrderQueryRepository orderQuery)
        {
            this.orderQuery = orderQuery;
        }
        public async Task<IOrder> Get(long id)
        {
            return await orderQuery.Get(o=>o.Id == id);
        }
    }
}
