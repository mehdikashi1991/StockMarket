using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Domain.Events;
using Framework.Contracts.UnitOfWork;
using L02Application;

namespace Application.OrderService.OrderCommandHandlers
{
    public class AddOrderCommandHandlers : StockMarketCommandHandler<AddOrderCommand>, ICommandHandler<AddOrderCommand>
    {
        private readonly IDispatcher<OrderCreated> OrderDispatcher;
        public AddOrderCommandHandlers(IUnitOfWork unitOfWork,
                                       IStockMarketFactory stockMarketFactory,
                                       IOrderCommandRepository orderCommandRepository,
                                       IOrderQueryRepository orderQueryRepository,
                                       ITradeCommandRepository tradeCommandRepository,
                                       ITradeQueryRespository tradeQueryRespository,
                                       IDispatcher<OrderCreated> orderDispatcher) :
            base(stockMarketFactory, orderCommandRepository, orderQueryRepository, tradeCommandRepository, tradeQueryRespository)
        {
            OrderDispatcher = orderDispatcher;
        }
        protected async override Task<ProcessedOrder> SpecificHandle(AddOrderCommand? command)
        {
            var result = await _stockMarketMatchEngine.ProcessOrderAsync(command.Price, command.Amount, command.Side, command.ExpDate, command.IsFillAndKill, command.orderParentId);

            await _orderCommandRepository.Add(result.Order);

            foreach (var domainEvent in result.Order.DomainEvents)
            {
                OrderDispatcher.Dispatch((OrderCreated)domainEvent);
            }

            foreach (var order in result.ModifiedOrders)
            {
                var findOrder = await _orderCommandRepository.Find(order.Id);
                findOrder.UpdateBy(order);


            }

            foreach (var trade in result.CreatedTrades)
            {
                await _tradeCommandRepository.Add(trade);
            }

            var processedOrder = new ProcessedOrder() { OrderId = result.Order == null ? 0 : result.Order.Id, Trades = result.CreatedTrades };

            return processedOrder;
        }
    }
}
