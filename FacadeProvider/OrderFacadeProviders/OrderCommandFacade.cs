using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Domain;
using Facade.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeProvider.OrderFacadeProviders
{
    public class OrderCommandFacade : IOrderCommandFacade
    {
        private readonly ICommandHandler<AddOrderCommand> addOrderCommandHandlers;
        private readonly IModifieOrderCommandHandler modifieOrderCommandHandler;
        private readonly ICancellOrderCommandHandler cancellOrderCommandHandler;
        private readonly ICancellAllOrdersCommandHandler cancellAllOrderCommandHandler;
        
        public OrderCommandFacade
            (
            IAddOrderCommandHandlers addOrderCommandHandlers,
            IModifieOrderCommandHandler modifieOrderCommandHandler,
            ICancellOrderCommandHandler cancellOrderCommandHandler,
            ICancellAllOrdersCommandHandler cancellAllOrderCommandHandler
            )
        {
            this.addOrderCommandHandlers = addOrderCommandHandlers;
            this.modifieOrderCommandHandler = modifieOrderCommandHandler;
            this.cancellOrderCommandHandler = cancellOrderCommandHandler;
            this.cancellAllOrderCommandHandler = cancellAllOrderCommandHandler;
        }
        public async Task<ProcessedOrder> CancelAllOrders(object obj)
        {
            return await cancellAllOrderCommandHandler.Handle(null);

        }

        public async Task<ProcessedOrder> CancelOrder(long id)
        {
            return await cancellOrderCommandHandler.Handle(id);
        }


        public async Task<ProcessedOrder> ModifyOrder(ModifieOrderCommand orderCommand)
        {
            return await modifieOrderCommandHandler.Handle(orderCommand);
        }

        public async Task<ProcessedOrder> ProcessOrder(AddOrderCommand orderCommand)
        {
            return await addOrderCommandHandlers.Handle(orderCommand);
        }
    }
}
