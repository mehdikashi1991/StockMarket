using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Messages
{
    public class AddOrderCommandMessage:ICommand
    {

        public Side Side { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        public bool? IsFillAndKill { get; set; } = false;

        public DateTime ExpireTime { get; set; }
    }
}
