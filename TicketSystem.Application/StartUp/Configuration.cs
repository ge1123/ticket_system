using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicketSystem.Application.Common.Behaviors;


namespace TicketSystem.Application.StartUp;

public static class Configuration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(Configuration).Assembly); // 掃描 TicketSystem.Application 組件
            // 註冊 ValidationBehavior 作為管道行為
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        
        // 註冊 FluentValidation，掃描 Application 層中的驗證器
        services.AddValidatorsFromAssembly(typeof(Configuration).Assembly);
        return services;
    }
}