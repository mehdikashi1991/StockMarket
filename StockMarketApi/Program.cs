using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection;
using Facade.Contract;
using FacadeProvider.OrderFacadeProviders;
using FacadeProvider.TradeFacadeProvider;
using Infrastructure;

namespace StockMarketApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new WindsorServiceProviderFactory());
            builder.Host.ConfigureContainer<WindsorContainer>(c =>
            {
                c.WindsorDependencyHolder();
            });

            builder.Host.UseNServiceBus(ctx =>
            {
                var endpointConfiguration = new EndpointConfiguration("StockMarketService");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();

                return endpointConfiguration;

            });


            builder.Services.DependencyHolder();
            builder.Services.AddScoped<IOrderCommandFacade, OrderCommandFacade>();
            builder.Services.AddScoped<IOrderQueryFacade, OrderQueryFacade>();
            builder.Services.AddScoped<ITradeQueryFacade, TradeQueryFacade>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMvc();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapControllers();
            app.Run();
        }

    }
}