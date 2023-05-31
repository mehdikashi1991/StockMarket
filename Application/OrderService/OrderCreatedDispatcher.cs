using Domain.Events;
using Framework.Contracts.Common;
using Framework.Contracts.Events;

namespace Application.OrderService
{
    public class OrderCreatedDispatcher : GenericDispatche<OrderCreated>
    {
        private readonly IServiceFactory serviceFactory;

        public OrderCreatedDispatcher(IServiceFactory serviceFactory) : base(serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }
    }



}
