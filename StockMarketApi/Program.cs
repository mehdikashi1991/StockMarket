using Facade.Contract;
using FacadeProvider.OrderFacadeProviders;
using FacadeProvider.TradeFacadeProvider;
using Infrastructure;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using NServiceBus.Extensions.Logging;

namespace StockMarketApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                LogManager
                .Setup()
                .LoadConfigurationFromAppSettings();

                //NLog
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                builder.Host.UseNServiceBus(ctx =>
                {
                    Microsoft.Extensions.Logging.ILoggerFactory extensionsLoggerFactory = new NLogLoggerFactory();

                    NServiceBus.Logging.ILoggerFactory nservicebusLoggerFactory = new ExtensionsLoggerFactory(loggerFactory: extensionsLoggerFactory);

                    NServiceBus.Logging.LogManager.UseFactory(loggerFactory: nservicebusLoggerFactory);

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
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}