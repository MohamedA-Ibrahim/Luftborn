using Luftborn.Server.Core.Application.Common.Models;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Commands.CreateTodo;

public record CreateTodoCommand : IRequest<TodoDto>
{
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? DueDate { get; init; }
    public int Priority { get; init; }
} 