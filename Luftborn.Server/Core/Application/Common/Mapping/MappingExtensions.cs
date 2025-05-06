using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Domain.Entities;

namespace Luftborn.Server.Core.Application.Common.Mapping;

public static class MappingExtensions
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            DueDate = todo.DueDate,
            Priority = todo.Priority,
            Created = todo.Created,
            LastModified = todo.LastModified
        };
    }

    public static IEnumerable<TodoDto> ToDto(this IEnumerable<Todo> todos)
    {
        return todos.Select(t => t.ToDto());
    }
} 