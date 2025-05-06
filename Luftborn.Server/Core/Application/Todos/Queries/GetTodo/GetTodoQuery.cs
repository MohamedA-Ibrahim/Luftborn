using Luftborn.Server.Core.Application.Common.Models;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Queries.GetTodo;

public record GetTodoQuery(Guid Id) : IRequest<TodoDto?>; 