using Domain.Contract.Orders.Repository.Query;
using Domain.Orders.Entities;
using Infrastructure.GenericServices;

namespace Infrastructure.Orders.QueryRepositories
{
    public class OrderQueryRepository : QueryRepository<Order, IOrder>, IOrderQueryRepository
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public OrderQueryRepository(TradeMatchingEngineContext tradeMatchingEngineContext) : base(tradeMatchingEngineContext)
        {
            this.tradeMatchingEngineContext = tradeMatchingEngineContext;
        }
    }


}
