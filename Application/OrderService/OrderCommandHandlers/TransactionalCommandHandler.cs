using Application.Contract.CommandHandlerContracts;
using Application.Factories;
using Domain;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Framework.Contracts.UnitOfWork;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Application.OrderService.OrderCommandHandlers
{
    public class TransactionalCommandHandler<T1> : ICommandHandler<T1> 
    {
        private readonly ICommandHandler<T1> handler;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionalCommandHandler(ICommandHandler<T1> handler, IUnitOfWork unitOfWork)
        {
            this.handler = handler;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProcessedOrder?> Handle(T1 command)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel=IsolationLevel.ReadCommitted},TransactionScopeAsyncFlowOption.Enabled);
            await _unitOfWork.OpenConnectionAsync();

            var result = await handler.Handle(command);

            await _unitOfWork.SaveChange();
             scope.Complete();
            return result;
        }

        

    }
}