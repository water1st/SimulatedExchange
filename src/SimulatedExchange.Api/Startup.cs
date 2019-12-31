using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using SimulatedExchange.Api.Filters;
using SimulatedExchange.Api.Hubs;
using SimulatedExchange.Applications;
using SimulatedExchange.ClientAdapter;
using SimulatedExchange.Commands;
using SimulatedExchange.DataAccess;
using SimulatedExchange.DataAccess.Memory;
using SimulatedExchange.Domain;
using SimulatedExchange.Queries;
using System;
using System.IO;
using System.Reflection;

namespace SimulatedExchange.Api
{
    public class Startup
    {
        private const string CORS_POLICY = "Default";
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

            services.AddSignalR();

            services.AddApi();
            services.AddApplications();
            services.AddCommands();
            services.AddQueries();
            services.AddDomain();
            services.AddClientAdapterCore();
            services.AddDataAccessCore();
            if (Environment.GetEnvironmentVariable("RUN_AT_LOCAL")?.ToUpper() == "TRUE")
            {
                services.AddMemoryProvider();
            }
            else
            {
                services.AddMySQLProvider(options =>
                {
                    options.EventSourcingDbConnectionString = Configuration.GetConnectionString("MySQL_EventSourcingDb");
                    options.ReporttingDbConnectionString = Configuration.GetConnectionString("MySQL_ReportingDb");
                });
            }

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

                builder.AddSerilog(logger);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Simulated Exchange Api", Version = "v1" });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(CORS_POLICY, policyOptions =>
                {
                    policyOptions.SetIsOriginAllowed(origin => true);
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CORS_POLICY);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TradeReportHub>("api/TRADE_REPORT_CHANNEL");
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Simulated Exchange Api");
            });


        }
    }
}
