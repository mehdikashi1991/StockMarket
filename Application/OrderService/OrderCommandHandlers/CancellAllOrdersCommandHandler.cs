using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Domain.Orders.Entities;
using Framework.Contracts.UnitOfWork;

namespace Application.OrderService.OrderCommandHandlers
{

    public class CancellAllOrdersCommandHandler : StockMarketCommandHandler<CancelAllOrderCommand>, ICommandHandler<CancelAllOrderCommand>
    {
        public CancellAllOrdersCommandHandler(IUnitOfWork unitOfWork, IStockMarketFactory stockMarketFactory,
            IOrderCommandRepository orderCommandRepository,
            IOrderQueryRepository orderQueryRepository,
            ITradeCommandRepository tradeCommandRepository,
            ITradeQueryRespository tradeQueryRespository) : base(stockMarketFactory, orderCommandRepository, orderQueryRepository, tradeCommandRepository, tradeQueryRespository)
        {
        }

        protected async override Task<ProcessedOrder> SpecificHandle(CancelAllOrderCommand command)
        {
            var allOrders = await _orderQuery.GetAll(x => x.Amount != 0 && x.OrderState != OrderStates.Cancell).ConfigureAwait(false);

            IStockMarketMatchingEngineProcessContext processedOrder;

            var orderIdList = new List<long>();

            foreach (var item in allOrders)
            {
                processedOrder = await this._stockMarketMatchEngine.CancelOrderAsync(item.Id).ConfigureAwait(false);

                foreach (var order in processedOrder.ModifiedOrders)
                {
                    var findOrder = await this._orderCommandRepository.Find(order.Id).ConfigureAwait(false);
                    findOrder.UpdateBy(order);
                    orderIdList.Add(order.Id);
                }
            }

            return new ProcessedOrder() { CancelledOrders = orderIdList };
        }
    }
}
