using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using uPromis.Microservice.ProjectAPI.Models;

namespace uPromis.Microservice.ProjectAPI
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
            services.AddTransient<uPromis.Services.IBusinessLoggingService, uPromis.Services.BusinessLoggingService>();

            services.AddTransient<IProgrammeRepository, ProgrammeRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IWorkpackageRepository, WorkpackageRepository>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IPlanTypeRepository, PlanTypeRepository>();

            services.AddTransient<Business.IProgrammeBusinessRules, Business.ProgrammeBusinessRules>();
            services.AddTransient<Business.IProjectBusinessRules, Business.ProjectBusinessRules>();
            services.AddTransient<Business.IWorkpackageBusinessRules, Business.WorkpackageBusinessRules>();
            services.AddTransient<Business.IActivityBusinessRules, Business.ActivityBusinessRules>();
            services.AddTransient<Business.IPlanTypeBusinessRules, Business.PlanTypeBusinessRules>();

            services.AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                }));
            });
            services.AddMassTransitHostedService();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddMvcOptions(o => o.EnableEndpointRouting = false) // needed to be able to access the url via browser
                .AddNewtonsoftJson()
                ;
            

            // added logging
            services.AddLogging();

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
                    policy.RequireClaim("scope", "api5");
                });
                options.AddPolicy("CanEditProjects",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion( context => 
                            context.User.HasClaim("IsProjectOwner", "true")
                            || context.User.HasClaim("IsProjectParticipant", "true") );
                    });
                options.AddPolicy("CanAccessProjects",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true")
                           || context.User.HasClaim("IsProjectReader", "true")
                           );
                    });

                options.AddPolicy("CanEditPlanTypes",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true"));
                    });
                options.AddPolicy("CanAccessPlanTypes",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true")
                           || context.User.HasClaim("IsProjectReader", "true")
                           );
                    });

                options.AddPolicy("CanEditActivities",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true"));
                    });
                options.AddPolicy("CanAccessActivities",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true")
                           || context.User.HasClaim("IsProjectReader", "true")
                           );
                    });

                options.AddPolicy("CanEditWorkpackages",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true"));
                    });
                options.AddPolicy("CanAccessWorkpackages",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           || context.User.HasClaim("IsProjectParticipant", "true")
                           || context.User.HasClaim("IsProjectReader", "true")
                           );
                    });

                options.AddPolicy("CanEditProgrammes",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           );
                    });
                options.AddPolicy("CanAccessProgrammes",
                    p =>
                    {
                        p.RequireAuthenticatedUser()
                        .RequireAssertion(context =>
                           context.User.HasClaim("IsProjectOwner", "true")
                           );
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

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
