using Framework.Contracts.Common;
using L02Application;

namespace Framework.Contracts.Events
{
    public abstract class GenericDispatche<T> : IDispatcher<T>
        where T : IEvent
    {
        private readonly IServiceFactory serviceFactory;

        public GenericDispatche(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public virtual void Dispatch(T domainEvent)
        {
            var services = serviceFactory.GetServices<IEventHandler<T>>();
            foreach (var service in services)
            {
                service.Handle(domainEvent);
            }
        }
    }


}

