using Application.Factories;
using Application.OrderService.OrderCommandHandlers;
using System.Threading.Tasks;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Framework.Contracts.UnitOfWork;

namespace Application.Tests
{
    public class TestCommandHandler : StockMarketCommandHandler<TestCommand>,ICallCounter
    {
        public int CallCount { get; set; }

        public TestCommandHandler(IUnitOfWork unitOfWork, IStockMarketFactory stockMarketFactory, 
            IOrderCommandRepository orderCommandRepository, 
            IOrderQueryRepository orderQueryRepository,
            ITradeCommandRepository tradeCommandRepository, 
            ITradeQueryRespository tradeQueryRespository) : base( stockMarketFactory, orderCommandRepository, orderQueryRepository, tradeCommandRepository, tradeQueryRespository)
        {
        }

        protected override Task<ProcessedOrder> SpecificHandle(TestCommand? command)
        {
            CallCount++; 
            return Task.FromResult(new ProcessedOrder());
        }
    }
}