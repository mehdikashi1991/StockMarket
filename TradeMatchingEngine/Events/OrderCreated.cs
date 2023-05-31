using Domain.Orders.Entities;
using Framework.Contracts.Events;

namespace Domain.Events
{
    public class OrderCreated : IEvent
    {
        public IOrder Order { get; }
        public OrderCreated(IOrder order)
        {
            Order = order;
        }
    }
}
