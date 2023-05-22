using FacadeProvider;
using Messages;

namespace MessageHandlers
{
    internal class ModifyOrderCommandHandler : IHandleMessages<ModifyOrderCommand>
    {

        private readonly IOrderQueryFacade _orderQueryFacade;
        private readonly IOrderCommandFacade orderFacade;

        public ModifyOrderCommandHandler

            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

        }
        public async Task Handle(ModifyOrderCommand message, IMessageHandlerContext context)
        {
            var command = new Application.Contract.Commands.ModifieOrderCommand()
            {
                OrderId = message.OrderId,
                Amount = message.Amount,
                Price = message.Price,
                ExpDate = message.ExpDate
            };

           var result= await orderFacade.ModifyOrder(command);

            await context.Send(result);
        }
    }
}
