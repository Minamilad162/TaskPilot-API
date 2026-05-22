using Microsoft.Extensions.DependencyInjection;
using ProjectTaskManagement.Application.Projects.Services;

namespace ProjectTaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }
}
