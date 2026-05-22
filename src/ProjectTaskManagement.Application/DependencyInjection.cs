using Microsoft.Extensions.DependencyInjection;
using ProjectTaskManagement.Application.Projects.Services;
using ProjectTaskManagement.Application.Tasks.Services;

namespace ProjectTaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectTaskService, ProjectTaskService>();

        return services;
    }
}
