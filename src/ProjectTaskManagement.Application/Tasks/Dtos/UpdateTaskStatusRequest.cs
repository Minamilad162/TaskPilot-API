using System.ComponentModel.DataAnnotations;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Tasks.Dtos;

public sealed record UpdateTaskStatusRequest(
    [EnumDataType(typeof(ProjectTaskStatus))] ProjectTaskStatus Status);
