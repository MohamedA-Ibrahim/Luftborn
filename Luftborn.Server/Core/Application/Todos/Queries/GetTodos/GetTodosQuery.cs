using Luftborn.Server.Core.Application.Common.Models;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Queries.GetTodos;

public record GetTodosQuery : IRequest<IEnumerable<TodoDto>>; 