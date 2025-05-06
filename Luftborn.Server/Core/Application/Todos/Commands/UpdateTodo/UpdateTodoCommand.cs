using Luftborn.Server.Core.Application.Common.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace Luftborn.Server.Core.Application.Todos.Commands.UpdateTodo;

public record UpdateTodoCommand : IRequest<TodoDto?>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? DueDate { get; init; }
    public int Priority { get; init; }
} 