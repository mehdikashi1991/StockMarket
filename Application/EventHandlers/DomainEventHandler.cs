using Domain.Events;
using Framework.Contracts.Events;
using Microsoft.Extensions.Logging;

namespace Application.EventHandlers
{
    public class DomainEventHandler :
        IDomainEventHandler<OrderCreated>

    {
        private readonly ILogger logger;
        public DomainEventHandler(ILogger<DomainEventHandler> logger)
        {
            this.logger = logger;
        }

        public void Handle(OrderCreated Event)
        {
            logger.LogCritical("Order Created Event Handled By FirstOrder{} With ID: {} \n", nameof(DomainEventHandler), Event.Order.Id);
        }


    }
}
