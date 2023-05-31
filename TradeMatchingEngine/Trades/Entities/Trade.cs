

using Framework.Contracts.Common;

namespace Domain.Trades.Entities
{
    public class Trade : AggegateRoot, ITrade
    {
        internal Trade(long id, long buyOrderId, long sellOrderId, int amount, int price)
        {
            Id = id;
            // OwnerId = ownerId;
            BuyOrderId = buyOrderId;
            SellOrderId = sellOrderId;
            Amount = amount;
            Price = price;
        }
        public override long Id { get; }
        //public int OwnerId { get;  }
        public long BuyOrderId { get; }
        public long SellOrderId { get; }
        public int Amount { get; }
        public int Price { get; }
    }
}
