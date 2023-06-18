using Application.Contract.CommandHandlerContracts;
using Domain;
using Framework.Contracts;
using Framework.Contracts.Events;
using Framework.Contracts.UnitOfWork;
using System.Reflection;

namespace Application.OrderService.OrderCommandHandlers
{
    public class TransactionalCommandHandler<T1> : ICommandHandler<T1>, IAsyncDisposable, IDisposable
    {
        private readonly ICommandHandler<T1> handler;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDispatcher dispatcher;
        private ITransactionService transactionService;
        private int counter;

        public TransactionalCommandHandler(ICommandHandler<T1> handler,
                                           IUnitOfWork unitOfWork,
                                           IDispatcher Dispatcher)
        {
            counter = 0;
            this.handler = handler;
            this.unitOfWork = unitOfWork;
            dispatcher = Dispatcher;
        }

        public async ValueTask DisposeAsync()
        {
            await unitOfWork.DisposeAsync();

            if (transactionService != null)
                await transactionService.DisposeAsync().ConfigureAwait(false);

        }

        public async Task<ProcessedOrder?> Handle(T1 command)
        {
            await beginTransaction().ConfigureAwait(false);

            var result = await handler.Handle(command).ConfigureAwait(false);

            var aggregateRoots = unitOfWork.GetModifiedAggregateRoots();
            await unitOfWork.SaveChange().ConfigureAwait(false);

            foreach (var aggregateRoot in aggregateRoots)
            {
                foreach (var domainEvent in aggregateRoot.DomainEvents.ToList())
                {
                    dispatcher.GetType().MakeGenericMethodByName("Dispatch", domainEvent.GetType())
                     .Invoke(dispatcher, new object[] { domainEvent });
                    aggregateRoot.RemoveDomainEvent(domainEvent);
                }
            }
            await commitTransaction().ConfigureAwait(false);
            return result;
        }

        private async Task commitTransaction()
        {
            Interlocked.Decrement(ref counter);
            if (counter == 0)
                await transactionService.CommitAsync().ConfigureAwait(false);
        }

        private async Task<ITransactionService> beginTransaction()
        {
            Interlocked.Increment(ref counter);
            if (transactionService != null) return transactionService;

            var lockObj = new SemaphoreSlim(1);
            await lockObj.WaitAsync().ConfigureAwait(false);
            if (transactionService != null) return transactionService;
            transactionService = await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            return transactionService;
        }

        public void Dispose()
        {
            this.DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
    public static class ReflectionHelper
    {
        public static MethodInfo MakeGenericMethodByName(this Type type, string methodName, Type genericArgumentType)
        {
            var dispatchMethodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(mi => mi.Name == methodName)
                                    .First();

            return dispatchMethodInfo.MakeGenericMethod(genericArgumentType);
        }
    }
}