using Application.Contract.CommandHandlerContracts;
using Domain;
using Framework.Contracts.UnitOfWork;
using L02Application;
using System.Reflection;
using System.Transactions;

namespace Application.OrderService.OrderCommandHandlers
{
    public class TransactionalCommandHandler<T1> : ICommandHandler<T1>
    {
        private readonly ICommandHandler<T1> handler;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDispatcher dispatcher;

        public TransactionalCommandHandler(ICommandHandler<T1> handler,
                                           IUnitOfWork unitOfWork,
                                           IDispatcher Dispatcher)
        {
            this.handler = handler;
            this.unitOfWork = unitOfWork;
            dispatcher = Dispatcher;
        }

        public async Task<ProcessedOrder?> Handle(T1 command)
        {
            using var transaction = new CommittableTransaction(new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted });
            await unitOfWork.OpenConnectionAsync();

            unitOfWork.EnlistTransaction(transaction);
            var result = await handler.Handle(command);

            var aggregateRoots = unitOfWork.GetModifiedAggregateRoots();
            await unitOfWork.SaveChange();

            foreach (var aggregateRoot in aggregateRoots)
            {
                foreach (var domainEvent in aggregateRoot.DomainEvents)
                {
                    MethodInfo genricMethod =
                        MakeGenericMethodByName(dispatcher.GetType(), "Dispatch", domainEvent.GetType());

                    genricMethod.Invoke(dispatcher, new object[] { domainEvent });


                    //dispatcher.Dispatch(domainEvent);
                }
            }
            transaction.Commit();
            return result;
        }

        private MethodInfo MakeGenericMethodByName(Type type, string methodName, Type genericArgumentType)
        {
            var dispatchMethodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(mi => mi.Name == methodName)
                                    .First();

            return dispatchMethodInfo.MakeGenericMethod(genericArgumentType);
        }
    }
}