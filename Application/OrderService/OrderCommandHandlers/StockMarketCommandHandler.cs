using Application.Contract.CommandHandlerContracts;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;

namespace Application.OrderService.OrderCommandHandlers
{
    public abstract class StockMarketCommandHandler<T1> : ICommandHandler<T1>
    {
        protected readonly IStockMarketFactory _stockMarketFactory;
        protected readonly IOrderCommandRepository _orderCommandRepository;
        protected readonly IOrderQueryRepository _orderQuery;
        protected readonly ITradeQueryRespository _tradeQuery;
        protected readonly ITradeCommandRepository _tradeCommandRepository;
        protected IStockMarketMatchEngineWithState _stockMarketMatchEngine;
        public StockMarketCommandHandler(IStockMarketFactory stockMarketFactory, IOrderCommandRepository orderCommandRepository, IOrderQueryRepository orderQueryRepository, ITradeCommandRepository tradeCommandRepository, ITradeQueryRespository tradeQueryRespository)
        {
            _stockMarketFactory = stockMarketFactory;
            _orderCommandRepository = orderCommandRepository;
            _orderQuery = orderQueryRepository;
            _tradeCommandRepository = tradeCommandRepository;
            _tradeQuery = tradeQueryRespository;
        }
        public async Task<ProcessedOrder?> Handle(T1 command)
        {
            _stockMarketMatchEngine = await _stockMarketFactory.GetStockMarket(_orderQuery, _tradeQuery).ConfigureAwait(false);
            return await SpecificHandle(command).ConfigureAwait(false);
        }
        protected abstract Task<ProcessedOrder?> SpecificHandle(T1 command);


    }
}