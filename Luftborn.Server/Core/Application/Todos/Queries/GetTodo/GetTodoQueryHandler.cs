using Luftborn.Server.Core.Application.Common.Mapping;
using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Domain.Interfaces;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Queries.GetTodo;

public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTodoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TodoDto?> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.TodoRepository.GetByIdAsync(request.Id);

        return todo?.ToDto();
    }
} 