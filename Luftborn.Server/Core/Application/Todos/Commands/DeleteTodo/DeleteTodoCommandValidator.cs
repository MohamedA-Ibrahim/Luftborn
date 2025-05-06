using FluentValidation;

namespace Luftborn.Server.Core.Application.Todos.Commands.DeleteTodo;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
} 