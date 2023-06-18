using Application.Contract.Commands;
using Facade.Contract;
using Messages;

namespace MessageHandlers
{
    public class OrderCommandsHandler :
        IHandleMessages<AddOrderCommandMessage>,
        IHandleMessages<ModifyOrderCommandMessage>,
        IHandleMessages<CancelOrderCommandMessage>,
        IHandleMessages<CancelAllOrderCommandMessage>

    {
        private readonly IOrderCommandFacade orderFacade;
        private readonly IOrderQueryFacade _orderQueryFacade;

        public OrderCommandsHandler

            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

        }

        public async Task Handle(AddOrderCommandMessage message, IMessageHandlerContext context)
        {
            var command = new Application.Contract.Commands.AddOrderCommand()
            {
                Amount = message.Amount,
                ExpDate = message.ExpireTime,
                Side = message.Side.ToDomain(),
                Price = message.Price,
                IsFillAndKill = (bool)message.IsFillAndKill,
            };

            var result = await orderFacade.ProcessOrder(command).ConfigureAwait(false);

        }

        public async Task Handle(ModifyOrderCommandMessage message, IMessageHandlerContext context)
        {
            var command = new Application.Contract.Commands.ModifieOrderCommand()
            {
                OrderId = message.OrderId,
                Amount = message.Amount,
                Price = message.Price,
                ExpDate = message.ExpDate
            };

            var result = await orderFacade.ModifyOrder(command).ConfigureAwait(false);


        }

        public async Task Handle(CancelOrderCommandMessage message, IMessageHandlerContext context)
        {

            var command = new CancelOrderCommand() { Id = message.Id };

            var result = await orderFacade.CancelOrder(command).ConfigureAwait(false);


        }

        public async Task Handle(CancelAllOrderCommandMessage message, IMessageHandlerContext context)
        {
            var command = new CancelAllOrderCommand() { };
            var result = await orderFacade.CancelAllOrders(command).ConfigureAwait(false);
        }
    }
}
