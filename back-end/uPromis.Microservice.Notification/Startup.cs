using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Quartz;
using MassTransit;
using Microsoft.OpenApi.Models;
using uPromis.Services.Queues;
using GreenPipes;
using Quartz.Impl;
using uPromis.Microservice.Notification.Data;
using uPromis.Microservice.Notification.Model;
using uPromis.Microservice.Notification.Job;

namespace uPromis.Microservice.Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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

            services.AddDbContext<NotificationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<INotificationRepository, NotificationRepository>();

            // add jobs to the DI pipeline to allow them to receive DBContext
            services.AddTransient<ContractJob>();
            services.AddTransient<ProjectJob>();
            services.AddTransient<ReminderJob>();

            _ = services.AddMassTransit(x =>
            {
                x.AddConsumer<Controllers.AddNotificationItemConsumer>();
                x.AddConsumer<Controllers.RemoveNotificationItemConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint(MessageBusQueueNames.ADDNOTIFYITEM, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r =>
                        {
                            r.Interval(2, 100);
                        });
                        ep.ConfigureConsumer<Controllers.AddNotificationItemConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint(MessageBusQueueNames.REMOVENOTIFYITEM, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r =>
                        {
                            r.Interval(2, 100);
                        });
                        ep.ConfigureConsumer<Controllers.RemoveNotificationItemConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            services.AddQuartz(q =>
            {
                // base quartz scheduler, job and trigger configuration
                q.UseMicrosoftDependencyInjectionJobFactory(
                    p => p.AllowDefaultConstructor = true
                );
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler scheduler = sf.GetScheduler().Result;

            // add the jobs if needed
            AddJob(scheduler, uPromis.Services.Notification.NotificationType.CONTRACTNOTIFICATION);
            AddJob(scheduler, uPromis.Services.Notification.NotificationType.PROJECTNOTIFICATION);
            AddJob(scheduler, uPromis.Services.Notification.NotificationType.REMINDERNOTIFICATION);

            services.AddSingleton<IScheduler>(scheduler);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "uPromis.Microservice.Notification", Version = "v1" });
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            }
            );
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api3");
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    //    builder.WithOrigins("https://localhost:3001")
                    //        .AllowAnyMethod()
                    //        .AllowAnyHeader();
                });
            });

        }

        private static void AddJob(IScheduler scheduler, string jobName)
        {
            Log.Information($"Adding job {jobName}");
            if (scheduler.CheckExists(new JobKey(jobName)).Result == true)
            {
                Log.Information($"Job {jobName} does not yet exist - adding.");
                var job = JobBuilder.Create<ContractJob>()
                    .WithIdentity(jobName)
                    .StoreDurably()
                    .Build();
                scheduler.AddJob(job, true);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "uPromis.Microservice.Notification v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope");
            });
        }
    }
}
