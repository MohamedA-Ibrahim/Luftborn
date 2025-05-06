using Luftborn.Server.Core.Domain.Interfaces;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(request.Id);

        if (todo == null)
            return false;

        await _unitOfWork.TodoRepository.DeleteAsync(todo);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
} 