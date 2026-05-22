using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Common.Models;
using ProjectTaskManagement.Application.Tasks.Dtos;
using ProjectTaskManagement.Application.Tasks.Services;

namespace ProjectTaskManagement.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1")]
public sealed class TasksController(IProjectTaskService taskService) : ControllerBase
{
    [HttpPost("projects/{projectId:guid}/tasks")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await taskService.CreateAsync(projectId, request, cancellationToken);
        return CreatedAtAction(nameof(GetByProject), new { projectId }, ApiResponse<TaskDto>.Ok(task, "Task created successfully."));
    }

    [HttpGet("projects/{projectId:guid}/tasks")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyCollection<TaskDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var tasks = await taskService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(ApiResponse<IReadOnlyCollection<TaskDto>>.Ok(tasks));
    }

    [HttpPatch("tasks/{id:guid}/status")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var task = await taskService.UpdateStatusAsync(id, request, cancellationToken);
        return Ok(ApiResponse<TaskDto>.Ok(task, "Task status updated successfully."));
    }

    [HttpDelete("tasks/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await taskService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
