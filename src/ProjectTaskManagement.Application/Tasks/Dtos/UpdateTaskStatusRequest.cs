using System.ComponentModel.DataAnnotations;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Tasks.Dtos;

public sealed record UpdateTaskStatusRequest(
    [property: EnumDataType(typeof(ProjectTaskStatus))] ProjectTaskStatus Status);
