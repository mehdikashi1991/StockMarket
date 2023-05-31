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
        private readonly ICommandHandler<ModifieOrderCommand> modifyOrderCommandHandlers;
        private readonly ICommandHandler<CancelOrderCommand> cancelOrderCommandHandlers;
        private readonly ICommandHandler<CancelAllOrderCommand> cancelAllOrderCommandHandler;

        public OrderCommandFacade(ICommandHandler<AddOrderCommand> addOrderCommandHandlers,
                                  ICommandHandler<ModifieOrderCommand> modifyOrderCommandHandlers,
                                  ICommandHandler<CancelOrderCommand> cancelOrderCommandHandlers,
                                  ICommandHandler<CancelAllOrderCommand> cancelAllOrderCommandHandler)
        {
            this.addOrderCommandHandlers = addOrderCommandHandlers;
            this.modifyOrderCommandHandlers = modifyOrderCommandHandlers;
            this.cancelOrderCommandHandlers = cancelOrderCommandHandlers;
            this.cancelAllOrderCommandHandler = cancelAllOrderCommandHandler;
        }

        public async Task<ProcessedOrder> CancelAllOrders(object obj)
        {
            return await cancelAllOrderCommandHandler.Handle(null);

        }

        public async Task<ProcessedOrder> CancelOrder(CancelOrderCommand command)
        {
            return await cancelOrderCommandHandlers.Handle(command);
        }


        public async Task<ProcessedOrder> ModifyOrder(ModifieOrderCommand orderCommand)
        {
            return await modifyOrderCommandHandlers.Handle(orderCommand);
        }

        public async Task<ProcessedOrder> ProcessOrder(AddOrderCommand orderCommand)
        {
            return await addOrderCommandHandlers.Handle(orderCommand);
        }
    }
}
