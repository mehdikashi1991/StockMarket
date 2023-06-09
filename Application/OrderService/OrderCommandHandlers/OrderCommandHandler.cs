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
    public abstract class CommandHandler<T1> : ICommandHandler<T1>
    {
        protected readonly IStockMarketFactory _stockMarketFactory;
        protected readonly IOrderCommandRepository _orderCommandRepository;
        protected readonly IOrderQueryRepository _orderQuery;
        protected readonly ITradeQueryRespository _tradeQuery;
        protected readonly ITradeCommandRepository _tradeCommandRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected IStockMarketMatchEngineWithState _stockMarketMatchEngine;
        public CommandHandler(IUnitOfWork unitOfWork, IStockMarketFactory stockMarketFactory, IOrderCommandRepository orderCommandRepository, IOrderQueryRepository orderQueryRepository, ITradeCommandRepository tradeCommandRepository, ITradeQueryRespository tradeQueryRespository)
        {
            _stockMarketFactory = stockMarketFactory;
            _orderCommandRepository = orderCommandRepository;
            _orderQuery = orderQueryRepository;
            _tradeCommandRepository = tradeCommandRepository;
            _tradeQuery = tradeQueryRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProcessedOrder?> Handle(T1 command)
        {
            _stockMarketMatchEngine = await _stockMarketFactory.GetStockMarket(_orderQuery, _tradeQuery);

            var result = await SpecificHandle(command);

            await _unitOfWork.SaveChange();

            return result;
        }

        protected abstract Task<ProcessedOrder> SpecificHandle(T1? command);

    }
}