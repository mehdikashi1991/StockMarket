using Castle.Windsor;
using Framework.Contracts.Common;

namespace Infrastructure.GenericServices
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IWindsorContainer container;

        public ServiceFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public IEnumerable<T> GetServices<T>()
        {
            var typ = typeof(T);
            var services = container.ResolveAll<T>();
            return services;
        }
    }
}
