using Autofac;
using Funzone.BuildingBlocks.EventBusDapr;
using Funzone.BuildingBlocks.Infrastructure.EventBus;
using Funzone.PhotoAlbums.Application.IntegrationEvents.EventHandling;
using Funzone.PhotoAlbums.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Funzone.PhotoAlbums.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IServiceCollection ServiceCollection { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddDapr();
            services.AddSingleton<IEventBus, DaprEventBus>();
            services.AddSingleton(Log.Logger);
            services.AddTransient<UserRegisteredIntegrationEventHandler>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funzone.PhotoAlbums.Api", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "http://172.16.100.175:5203";
                options.RequireHttpsMetadata = false;
                options.Audience = "photoAlbumsApi";
            });

            ServiceCollection = services;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var serviceProvider = ServiceCollection.BuildServiceProvider();
            var connectionString = Configuration.GetConnectionString("MySql");
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            var logger = serviceProvider.GetRequiredService<ILogger>();

            builder.RegisterModule(
                new PhotoAlbumModule(
                    connectionString,
                    logger,
                    eventBus));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funzone.PhotoAlbums.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
            });
        }
    }
}
