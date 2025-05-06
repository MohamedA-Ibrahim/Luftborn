using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Commands.DeleteTodo;

public record DeleteTodoCommand(Guid Id) : IRequest<bool>; 