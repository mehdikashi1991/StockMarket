using Application.Contract;
using Application.Factories;
using Application.OrderService.OrderCommandHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Application.Contract.CommandHandlerContracts;
using Infrastructure.Orders.QueryRepositories;
using Infrastructure.Trades.CommandRepositories;
using Infrastructure.Trades.QueryRepositories;
using Infrastructure.Orders.CommandRepositories;
using Framework.Contracts.UnitOfWork;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using Application.Contract.Commands;

namespace Infrastructure
{
    public static class BusinessDependencies
    {
        public static void WindsorDependencyHolder(this IWindsorContainer container)
        {
            container.Register(
           Component.For<ICommandHandler<AddOrderCommand>>().ImplementedBy<AddOrderCommandHandlers>()
           .Named(typeof(AddOrderCommandHandlers).FullName).LifeStyle.ScopedToNetServiceScope(),
           //Component.For<ICommandHandler<>>().ImplementedBy<TradeCommandRepository>()
           //.Named(typeof(TradeCommandRepository).FullName).LifeStyle.ScopedToNetServiceScope(),
           Component.For<ICommandHandler<ModifieOrderCommand>>().ImplementedBy<ModifieOrderCommandHandler>()
           .Named(typeof(ModifieOrderCommandHandler).FullName).LifeStyle.ScopedToNetServiceScope(),
           Component.For<ICommandHandler<CancelOrderCommand>>().ImplementedBy<CancellOrderCommandHandler>()
           .Named(typeof(CancellOrderCommandHandler).FullName).LifeStyle.ScopedToNetServiceScope(),
           Component.For<ICommandHandler<CancelAllOrderCommand>>().ImplementedBy<CancellAllOrdersCommandHandler>()
           .Named(typeof(CancellAllOrdersCommandHandler).FullName).LifeStyle.ScopedToNetServiceScope()

           // Component.For<ICancellAllOrdersCommandHandler>()
           // .ImplementedBy<TransactionalCommandHandler<object, ICancellAllOrdersCommandHandler>>()
           //.LifeStyle.ScopedToNetServiceScope(),

           //Component.For<MyServiceA>()
           //.DependsOn(Dependency.OnComponent<IMyService, ServiceA>()).LifestyleSingleton(),
           //Component.For<MyServiceB>()
           //.DependsOn(Dependency.OnComponent<IMyService, ServiceB>()).LifestyleSingleton(),
           //Component.For(typeof(IServiceFactory<>))
           //.ImplementedBy(typeof(ServiceFactory<>)).LifestyleTransient(),
           //Component.For(typeof(IServiceFactory))
           //.ImplementedBy(typeof(ServiceFactory)).LifestyleSingleton()

           );
        }
        public static IServiceCollection DependencyHolder(this IServiceCollection services)
        {
            services.AddDbContext<TradeMatchingEngineContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=TradeMatchingEngine;Integrated Security=true;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
            services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
           // services.AddScoped<IAddOrderCommandHandlers, AddOrderCommandHandlers>();
            //services.AddScoped<ITradeCommandRepository, TradeCommandRepository>();
            //builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITradeQueryRespository, TradeQueryRepository>();
            services.AddSingleton<IStockMarketFactory, StockMarketFactory>();
            //services.AddScoped<IModifieOrderCommandHandler, ModifieOrderCommandHandler>();
            //services.AddScoped<ICancellOrderCommandHandler, CancellOrderCommandHandler>();
            //services.AddScoped<ICancellAllOrdersCommandHandler, CancellAllOrdersCommandHandler>();
            return services;
        }
    }
}
