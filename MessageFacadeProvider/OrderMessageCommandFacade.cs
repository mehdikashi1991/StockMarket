using Application.Contract.Commands;
using Domain;
using Facade.Contract;
using Framework.Contracts;
using Messages;

namespace MessageFacadeProvider
{
    public class OrderMessageCommandFacade : IOrderCommandFacade, IAsyncDisposable, IDisposable
    {
        IMessageService messageService;
        public OrderMessageCommandFacade(IMessageService messageService)
        {
            this.messageService = messageService;
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

            await messageService.SendMessageAsync(message).ConfigureAwait(false);

            return new ProcessedOrder();
        }

        public async Task<ProcessedOrder> CancelAllOrders(object obj)
        {
            var message = new CancelAllOrderCommandMessage();
            await messageService.SendMessageAsync(message).ConfigureAwait(false);

            return new ProcessedOrder();
        }



        public async Task<ProcessedOrder> CancelOrder(CancelOrderCommand command)
        {
            var message = new CancelOrderCommandMessage() { Id = command.Id };

            await messageService.SendMessageAsync(message).ConfigureAwait(false);
            return new ProcessedOrder();
        }

        public void Dispose()
        {
            DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
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
            await messageService.SendMessageAsync(message).ConfigureAwait(false);
            return new ProcessedOrder();
        }


    }
}