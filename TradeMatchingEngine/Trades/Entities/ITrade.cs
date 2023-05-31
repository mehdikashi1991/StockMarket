using Framework.Contracts.Common;

namespace Domain.Trades.Entities
{
    public interface ITrade : IAggegateRoot
    {
        int Amount { get; }
        long BuyOrderId { get; }
        // int OwnerId { get; }
        int Price { get; }
        long SellOrderId { get; }
        long Id { get; }
    }
}