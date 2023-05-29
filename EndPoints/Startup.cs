using EndPoints.Controller;
using Facade.Contract;
using FacadeProvider.OrderFacadeProviders;
using FacadeProvider.TradeFacadeProvider;
using Infrastructure;
using System.Reflection;

namespace EndPoints
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //// Add services to the container.
            //builder.Services.AddRazorPages();
            services.DependencyHolder();
            services.AddScoped<IOrderQueryFacade, OrderQueryFacade>();
            services.AddScoped<IOrderCommandFacade, OrderCommandFacade>();
            services.AddScoped<ITradeQueryFacade, TradeQueryFacade>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMvc();
            services
                    .AddControllers()
                    .AddApplicationPart(typeof(OrdersController).Assembly)
                    .AddControllersAsServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
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
        }
    }
}
