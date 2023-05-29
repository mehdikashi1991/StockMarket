namespace Domain.Trades.Entities
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