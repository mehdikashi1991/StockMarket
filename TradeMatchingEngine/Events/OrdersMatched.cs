using Domain.Trades.Entities;
using Framework.Contracts.Events;

namespace Domain.Events
{
    public class OrdersMatched : IDomainEvent
    {
        public ITrade Trade { get; }
        public OrdersMatched(ITrade trade)
        {
            Trade = trade;
        }
    }
}
