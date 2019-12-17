using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SimulatedExchange.Api.Filters;
using SimulatedExchange.Api.Hubs;
using SimulatedExchange.Applications;
using SimulatedExchange.Commands;
using SimulatedExchange.DataAccess;
using SimulatedExchange.Domain;
using SimulatedExchange.Infrastructure;
using SimulatedExchange.Queries;

namespace SimulatedExchange.Api
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
            services.AddControllers(options =>

            {
                options.Filters.Add<ExceptionFilter>();
            });

            services.AddSignalRCore();

            services.AddApi();
            services.AddApplicationLayout();
            services.AddQueryLayout();
            services.AddCommandLayout();
            services.AddDomainLayout();
            services.AddDataAccessLayout();
            services.AddInfrastructureLayout();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

                builder.AddSerilog(logger);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TradeReportHub>("api/TRADE_REPORT_CHANNEL");
            });


        }
    }
}
