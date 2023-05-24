using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Domain;
using Facade.Contract;
using Framework.Contracts;
using Messages;

namespace MessageFacadeProvider
{
    public class OrderMessageCommandFacade:IOrderCommandFacade
    {
        IMessageService messageService;
        public OrderMessageCommandFacade(IMessageService messageService)
        {
            this.messageService = messageService;
        }
     

        public async Task<ProcessedOrder> CancelAllOrders(object obj)
        {
            var message = new CancelAllOrderCommandMessage();
            await messageService.SendMessageAsync(message);

            return new ProcessedOrder();
        }

       

        public async Task<ProcessedOrder> CancelOrder(long id)
        {
            var message=new CancelOrderCommandMessage() { Id=id};

            await messageService.SendMessageAsync(message);
            return new ProcessedOrder();
        }      
       

       public async Task<ProcessedOrder> ModifyOrder(ModifieOrderCommand orderCommand)
        {
            var message = new ModifyOrderCommandMessage()
            {
                OrderId = orderCommand.OrderId,
                Amount = orderCommand.Amount,
                ExpDate = orderCommand.ExpDate,
                Price = orderCommand.Price
            };
            await messageService.SendMessageAsync(message);
            return new ProcessedOrder();
        }

        public async Task<ProcessedOrder> ProcessOrder(AddOrderCommand orderCommand)
        {
            var message = new AddOrderCommandMessage()
            {
                Side = orderCommand.Side.ToMessage(),
                Amount = orderCommand.Amount,
                Price = orderCommand.Price,
                IsFillAndKill = orderCommand.IsFillAndKill,
                ExpireTime = (DateTime)orderCommand.ExpDate

            };

            await messageService.SendMessageAsync(message);

            return new ProcessedOrder();
        }
    }
}