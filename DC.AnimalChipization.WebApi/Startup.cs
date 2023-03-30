using System.Collections.Generic;
using DC.AnimalChipization.Application;
using DC.AnimalChipization.Data;
using DC.AnimalChipization.WebApi.Handlers;
using DC.AnimalChipization.WebApi.Mappings;
using DC.AnimalChipization.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DC.AnimalChipization.WebApi
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
            services.RegisterApplicationLogic();
            services.RegisterData(Configuration);

            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddAutoMapper(typeof(AccountProfile).Assembly);

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DC.AnimalChipization.WebApi", Version = "v1" });
                
                c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Basic",
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid credentials"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Basic"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DC.AnimalChipization.WebApi v1"));

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseHealthChecks("/healthcheck");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
