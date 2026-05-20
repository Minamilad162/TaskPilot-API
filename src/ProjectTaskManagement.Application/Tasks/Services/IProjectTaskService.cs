using ProjectTaskManagement.Application.Tasks.Dtos;

namespace ProjectTaskManagement.Application.Tasks.Services;

public interface IProjectTaskService
{
    Task<TaskDto> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TaskDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<TaskDto> UpdateStatusAsync(Guid id, UpdateTaskStatusRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
