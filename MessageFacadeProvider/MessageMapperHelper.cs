using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageFacadeProvider
{
    public static class MessageMapperHelper
    {
        public static Side ToMessage(this Domain.Orders.Entities.Side side)
        {
            switch (side)
            {
                case Domain.Orders.Entities.Side.Buy: return Side.Buy;
                default: return Side.Sell;
            }
        }
    }
}
