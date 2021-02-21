using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System;

namespace uPromis.Microservice.ReportingServer
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
            var logFile = Configuration.GetValue<string>("LogFile");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    logFile,
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();

            _ = services.AddMassTransit(x =>
              {
                  x.AddConsumer<Controllers.ContractSaveConsumer>();
                  x.AddConsumer<Controllers.ContractDeleteConsumer>();
                  x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                  {
                      cfg.UseHealthCheck(provider);
                      cfg.Host(new Uri("rabbitmq://localhost"), h =>
                      {
                          h.Username("guest");
                          h.Password("guest");
                      });
                      cfg.ReceiveEndpoint(uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERSAVECONTRACT, ep =>
                      {
                          ep.PrefetchCount = 16;
                          ep.UseMessageRetry(r =>
                          {
                              r.Interval(2, 100);
                          });
                          ep.ConfigureConsumer<Controllers.ContractSaveConsumer>(provider);
                      });
                      cfg.ReceiveEndpoint(uPromis.Services.Queues.MessageBusQueueNames.REPORTSERVERDELETECONTRACT, ep =>
                      {
                          ep.PrefetchCount = 16;
                          ep.UseMessageRetry(r =>
                          {
                              r.Interval(2, 100);
                          });
                          ep.ConfigureConsumer<Controllers.ContractDeleteConsumer>(provider);
                      });
                  }));
              });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "uPromis.Microservice.ReportingServer", Version = "v1" });
            });
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "uPromis.Microservice.ReportingServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
