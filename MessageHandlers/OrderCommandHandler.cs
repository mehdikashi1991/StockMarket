using FacadeProvider;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public class OrderCommandHandler : IHandleMessages<AddOrderCommand>
    {
        private readonly IOrderQueryFacade _orderQueryFacade;
        private readonly IOrderCommandFacade orderFacade;

        public  OrderCommandHandler

            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

        }

        public Task Handle(AddOrderCommand message, IMessageHandlerContext context)
        {
            throw new NotImplementedException();
        }
        //public Task Handle(AddOrderCommand message, IMessageHandlerContext context)
        //{
        //    var command = new AddOrderCommand()
        //    {
        //        Amount = orderVM.Amount,
        //        ExpDate = orderVM.ExpireTime,
        //        Side = orderVM.Side,
        //        Price = orderVM.Price,
        //        IsFillAndKill = (bool)orderVM.IsFillAndKill,
        //    };

        //    return CreatedAtAction(
        //       "ProcessOrder",
        //        "Orders",
        //        null,
        //        await orderFacade.ProcessOrder(command));
        //}
    }
}
