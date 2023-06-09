﻿using Application.Contract.CommandHandlerContracts;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Framework.Contracts.UnitOfWork;

namespace Application.OrderService.OrderCommandHandlers
{
    public class CancellOrderCommandHandler : CommandHandler<long>, ICancellOrderCommandHandler
    {
        public CancellOrderCommandHandler(IUnitOfWork unitOfWork, IStockMarketFactory stockMarketFactory, 
            IOrderCommandRepository orderCommandRepository, 
            IOrderQueryRepository orderQueryRepository, 
            ITradeCommandRepository tradeCommandRepository,
            ITradeQueryRespository tradeQueryRespository) : base(unitOfWork, stockMarketFactory, orderCommandRepository, orderQueryRepository, tradeCommandRepository, tradeQueryRespository)
        {
        }

        protected async override Task<ProcessedOrder> SpecificHandle(long orderId)
        {
            var result = await this._stockMarketMatchEngine.CancelOrderAsync(orderId);

            foreach (var order in result.ModifiedOrders)
            {
                var findOrder = await this._orderCommandRepository.Find(order.Id);
                findOrder.UpdateBy(order);
            }

            return new ProcessedOrder() { OrderId = result.ModifiedOrders == null ? 0 : result.ModifiedOrders.FirstOrDefault() == null ? 0 : result.ModifiedOrders.FirstOrDefault().Id } as ProcessedOrder;
        }
    }
}
