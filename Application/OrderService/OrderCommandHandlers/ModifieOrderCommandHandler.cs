using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Framework.Contracts.UnitOfWork;

namespace Application.OrderService.OrderCommandHandlers
{
    public class ModifieOrderCommandHandler : StockMarketCommandHandler<ModifieOrderCommand>, ICommandHandler<ModifieOrderCommand>
    {
        public ModifieOrderCommandHandler(IUnitOfWork unitOfWork,
                                          IStockMarketFactory stockMarketFactory,
                                          IOrderCommandRepository orderCommandRepository,
                                          IOrderQueryRepository orderQueryRepository,
                                          ITradeCommandRepository tradeCommandRepository,
                                          ITradeQueryRespository tradeQueryRespository)
            : base(stockMarketFactory,
                   orderCommandRepository,
                   orderQueryRepository,
                   tradeCommandRepository,
                   tradeQueryRespository)
        {
        }

        protected async override Task<ProcessedOrder> SpecificHandle(ModifieOrderCommand? command)
        {
            try
            {
                var result = await this._stockMarketMatchEngine.ModifieOrder(command.OrderId, command.Price, command.Amount, command.ExpDate).ConfigureAwait(false);

                await _orderCommandRepository.Add(result.Order).ConfigureAwait(false);

                foreach (var order in result.ModifiedOrders)
                {
                    var findOrder = await this._orderCommandRepository.Find(order.Id).ConfigureAwait(false);
                    findOrder.UpdateBy(order);
                }

                foreach (var trade in result.CreatedTrades)
                {
                    await _tradeCommandRepository.Add(trade).ConfigureAwait(false);
                }
                return new ProcessedOrder() { OrderId = result.Order == null ? 0 : result.Order.Id };
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
