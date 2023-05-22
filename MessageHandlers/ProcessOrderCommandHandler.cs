using FacadeProvider;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public class ProcessOrderCommandHandler : IHandleMessages<AddOrderCommand>
    {
        private readonly IOrderQueryFacade _orderQueryFacade;
        private readonly IOrderCommandFacade orderFacade;

        public  ProcessOrderCommandHandler

            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

        }

        public async Task Handle(AddOrderCommand message, IMessageHandlerContext context)
        {
            var command = new Application.Contract.Commands.AddOrderCommand()
            {
                Amount = message.Amount,
                ExpDate = message.ExpireTime,
                Side = message.Side,
                Price = message.Price,
                IsFillAndKill = (bool)message.IsFillAndKill,
            };
           
           var result= await orderFacade.ProcessOrder(command );

          await  context.Send(result);
        }
        
    }
}
