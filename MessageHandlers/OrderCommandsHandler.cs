using Facade.Contract;
using Messages;
using NServiceBus.Logging;

namespace MessageHandlers
{
    public class OrderCommandsHandler :
        IHandleMessages<AddOrderCommandMessage>,
        IHandleMessages<ModifyOrderCommandMessage>,
        IHandleMessages<CancelOrderCommandMessage>,
        IHandleMessages<CancelAllOrderCommandMessage>

    {
        private readonly IOrderQueryFacade _orderQueryFacade;
        private readonly IOrderCommandFacade orderFacade;
        private readonly ILog _logger;

        public OrderCommandsHandler

            (
            IOrderCommandFacade orderFacade,
            IOrderQueryFacade orderQueryFacade

            )
        {
            this.orderFacade = orderFacade;
            _orderQueryFacade = orderQueryFacade;

            _logger = LogManager.GetLogger<OrderCommandsHandler>();
        }

        public async Task Handle(AddOrderCommandMessage message, IMessageHandlerContext context)
        {
            _logger.Info("AddOrderCommandMessageHandler");
            var command = new Application.Contract.Commands.AddOrderCommand()
            {
                Amount = message.Amount,
                ExpDate = message.ExpireTime,
                Side = message.Side.ToDomain(),
                Price = message.Price,
                IsFillAndKill = (bool)message.IsFillAndKill,
            };

            var result = await orderFacade.ProcessOrder(command);
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

            var result = await orderFacade.ModifyOrder(command);
        }

        public async Task Handle(CancelOrderCommandMessage message, IMessageHandlerContext context)
        {
            var id = message.Id;

            var result = await orderFacade.CancelOrder(id);
        }

        public async Task Handle(CancelAllOrderCommandMessage message, IMessageHandlerContext context)
        {
            var result = await orderFacade.CancelAllOrders(null);
        }
    }
}