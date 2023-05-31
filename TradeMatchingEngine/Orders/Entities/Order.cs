using Domain.Events;
using Framework.Contracts.Common;

namespace Domain.Orders.Entities
{

    public class Order : AggegateRoot, IOrder
    {
        private OrderStates state;

        internal Order(long id, Side side, int price, int amount, DateTime expireTime, OrderStates? orderState, int? originalAmount = null, bool? isFillAndKill = null, long? orderParentId = null)
        {
            Id = id;
            Side = side;
            Price = price;
            Amount = amount;
            OriginalAmount = originalAmount ?? amount;
            IsFillAndKill = isFillAndKill;
            ExpireTime = expireTime;
            state = orderState == null ? OrderStates.Register : (OrderStates)orderState;
            OrderParentId = orderParentId;
            AddDomainEvent(new OrderCreated(this));
        }

        public OrderStates? OrderState { get { return state; } private set { value = state; } }
        public override long Id { get; }
        public Side Side { get; private set; }
        public int Price { get; private set; }
        public int? OriginalAmount { get; private set; }
        public int Amount { get; private set; }
        public bool? IsFillAndKill { get; private set; } = false;

        public bool HasCompleted
        {
            get
            {
                if (Amount <= 0) return true;

                return false;
            }
        }

        public DateTime ExpireTime { get; private set; }

        public bool IsExpired => ExpireTime < DateTime.Now;
        public OrderStates GetOrderState() => state;
        public long? OrderParentId { get; private set; }


        public int DecreaseAmount(int amount)
        {
            Amount = Amount - amount;

            if (Amount <= 0)
            {
                Amount = 0;
            }

            return Amount;
        }
        public void SetStateCancelled()
        {
            state = OrderStates.Cancell;
        }
        public void SetStateRegistered()
        {
            state = OrderStates.Register;
        }
        public void SetStateModified()
        {
            state = OrderStates.Modified;
        }
        public void UpdateBy(IOrder order)
        {
            Price = order.Price;
            OriginalAmount = order.OriginalAmount;
            Amount = order.Amount;
            ExpireTime = order.ExpireTime;
            IsFillAndKill = order.IsFillAndKill;
            Side = order.Side;
            state = (OrderStates)order.OrderState;
        }
        internal Order Clone(int originalAmount)
        {
            return new Order(Id, Side, Price, Amount, ExpireTime, OrderState, originalAmount, IsFillAndKill, OrderParentId);
        }
    }
}
