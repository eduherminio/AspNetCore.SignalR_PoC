using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignalRServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", b =>
                {
                    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddTransient<HubMethods<VehicleHub>>();
            services.AddSignalR();
            services.AddSingleton<ConnectionManager>();
            services.AddScoped<ISignalRService, SignalRService>();
        }
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");
            loggerFactory.AddConsole(LogLevel.Debug);

            app.UseFileServer();
            app.UseStaticFiles();
            app.UseSignalR(routes =>
            {
                routes.MapHub<VehicleHub>("/RealTimeVehicles");
            });

            // Workaround to insert ISignalRService into PositionGenerator singleton
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var signalRService = serviceScope.ServiceProvider.GetService<ISignalRService>();
                PositionGenerator.Instance.Config(signalRService);
            }
        }
    }
}
