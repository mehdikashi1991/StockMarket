using Framework.Contracts.Common;
using L02Application;

namespace Framework.Contracts.Events
{
    public class GenericDispatcher : IDispatcher
    {
        private readonly IServiceFactory serviceFactory;

        public GenericDispatcher(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public void Dispatch<T>(T domainEvent)
            where T : class, IDomainEvent
        {
            //var domainHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            //var genericGetServicesMethod = serviceFactory
            //    .GetType()
            //    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            //    .Where(mi => mi.Name == "GetServices").First();

            //var getServicesMethod = genericGetServicesMethod.MakeGenericMethod(domainHandlerType);

            //var services = (IEnumerable<object>)getServicesMethod.Invoke(serviceFactory, new object[] { });

            //foreach (var service in services)
            //{
            //    var handleMethod = domainHandlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            //                            .Where(mi => mi.Name == "Handle").First();
            //    handleMethod.Invoke(service, new object[] { domainEvent });
            //}
            var services = serviceFactory.GetServices<IDomainEventHandler<T>>();
            foreach (var service in services)
            {
                service.Handle(domainEvent);
            }
        }
    }


}

