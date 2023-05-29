﻿using Domain;
using Domain.Common;
using Domain.Contract.Orders.Repository.Query;

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
            return await orderQuery.Get(o => o.Id == id);
        }

        //public async Task<PageResult<Order>> GetAllWithPaging(int page, int pageSize, int currentPage = 1, long lastId = 0)
        //{
        //    return await orderQuery.GetPaged(page, pageSize, currentPage, lastId);
        //}
    }
}
