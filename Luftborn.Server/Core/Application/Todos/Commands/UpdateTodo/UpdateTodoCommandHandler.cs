using Luftborn.Server.Core.Application.Common.Mapping;
using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Domain.Interfaces;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Commands.UpdateTodo;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, TodoDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(request.Id);
        if (todo == null)
            return null;

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsCompleted = request.IsCompleted;
        todo.DueDate = request.DueDate;
        todo.Priority = request.Priority;

        await _unitOfWork.TodoRepository.UpdateAsync(todo);
        await _unitOfWork.SaveChangesAsync();

        return todo.ToDto();
    }
} 