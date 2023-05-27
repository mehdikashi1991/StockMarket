using Domain.Common;

namespace Domain
{
    public interface ITrade : IBaseEntity<long>
    {
        int Amount { get; }
        long BuyOrderId { get; }
       // int OwnerId { get; }
        int Price { get; }
        long SellOrderId { get; }
        long Id { get; }
    }
}