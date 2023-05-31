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
            return container.ResolveAll<T>();
        }
    }
}
