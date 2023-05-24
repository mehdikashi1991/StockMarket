using Microsoft.Extensions.DependencyInjection;

using Domain.Contract.Orders.Repository.Command;
using Domain.Orders.Entities;
using Infrastructure.GenericServices;
using Domain.Orders.Entities;
namespace Infrastructure.Orders.CommandRepositories
{
    public class OrderCommandRepository : CommandRepository<Order, IOrder>, IOrderCommandRepository
    {
        private readonly TradeMatchingEngineContext _tradeMatchingEngineContext;

        public OrderCommandRepository(TradeMatchingEngineContext dbcontext) : base(dbcontext)
        {
            _tradeMatchingEngineContext = dbcontext;
        }
    }
}
