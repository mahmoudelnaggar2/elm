using Domain.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {
        var Assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => { 
        configuration.RegisterServicesFromAssemblies(
            Assembly);
        });
        
        services.AddValidatorsFromAssembly(Assembly);

        return services;
    }
}
