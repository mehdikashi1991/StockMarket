using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class ModifyOrderCommand:ICommand
    {
        public long OrderId { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        public DateTime? ExpDate { get; set; }
    }
}
