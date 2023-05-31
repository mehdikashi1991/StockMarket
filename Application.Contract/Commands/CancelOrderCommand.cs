using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.Commands
{
    public class CancelOrderCommand : ICommand
    {
        public long Id { get; set; }
    }
}
