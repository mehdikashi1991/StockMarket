using Domain.Orders.Entities;

namespace Controllers.Model
{
    public class OrderVM
    {
        public long Id { get; set; }

        public Side Side { get; set; }

        public int Price { get; set; }

        public int Amount { get;  set; }

        public bool? IsFillAndKill { get; set; } = false;

        public DateTime ExpireTime { get; set; }

    }
}
