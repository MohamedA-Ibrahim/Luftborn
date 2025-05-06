using Luftborn.Server.Core.Application.Common.Mapping;
using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Domain.Entities;
using Luftborn.Server.Core.Domain.Interfaces;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Commands.CreateTodo;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted,
            DueDate = request.DueDate,
            Priority = request.Priority
        };

        await _unitOfWork.TodoRepository.AddAsync(todo);
        await _unitOfWork.SaveChangesAsync();

        return todo.ToDto();
    }
} 