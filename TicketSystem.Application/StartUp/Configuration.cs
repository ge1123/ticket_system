using Microsoft.Extensions.DependencyInjection;


namespace TicketSystem.Application.StartUp;

public static class Configuration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(Configuration).Assembly); // 掃描 TicketSystem.Application 組件
        });
        return services;
    }
}