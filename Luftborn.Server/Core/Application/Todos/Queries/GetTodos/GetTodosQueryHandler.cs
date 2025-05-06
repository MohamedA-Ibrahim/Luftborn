using Luftborn.Server.Core.Application.Common.Mapping;
using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Domain.Interfaces;
using MediatR;

namespace Luftborn.Server.Core.Application.Todos.Queries.GetTodos;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IEnumerable<TodoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTodosQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = await _unitOfWork.TodoRepository.GetAllAsync();
        return todos.ToDto();
    }
} 