using ProjectTaskManagement.Application.Projects.Dtos;

namespace ProjectTaskManagement.Application.Projects.Services;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProjectDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
