using Domain.Contract.Orders.Repository.Query;
using Infrastructure.GenericServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Order.CommandRepositories
{
    public class OrderQueryRepository :QueryRepository<Domain.Order, Domain.IOrder>, IOrderQueryRepository
    {
        private readonly TradeMatchingEngineContext tradeMatchingEngineContext;

        public OrderQueryRepository(TradeMatchingEngineContext tradeMatchingEngineContext):base(tradeMatchingEngineContext)
        {
            this.tradeMatchingEngineContext = tradeMatchingEngineContext;
        }
    }

    
}
