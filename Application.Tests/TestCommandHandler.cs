using Application.Factories;
using Application.OrderService.OrderCommandHandlers;
using System.Threading.Tasks;
using Domain;
using Domain.Orders.Repositories.Command;
using Domain.UnitOfWork;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;

namespace Application.Tests
{
    public class TestCommandHandler : CommandHandler<TestCommand>,ICallCounter
    {
        public int CallCount { get; set; }

        public TestCommandHandler(IUnitOfWork unitOfWork, IStockMarketFactory stockMarketFactory, 
            IOrderCommandRepository orderCommandRepository, 
            IOrderQueryRepository orderQueryRepository,
            ITradeCommandRepository tradeCommandRepository, 
            ITradeQueryRespository tradeQueryRespository) : base(unitOfWork, stockMarketFactory, orderCommandRepository, orderQueryRepository, tradeCommandRepository, tradeQueryRespository)
        {
        }

        protected override Task<ProcessedOrder> SpecificHandle(TestCommand? command)
        {
            CallCount++; 
            return Task.FromResult(new ProcessedOrder());
        }
    }
}