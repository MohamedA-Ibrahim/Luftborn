using Luftborn.Server.Core.Application.Common.Models;
using Luftborn.Server.Core.Application.Todos.Commands.CreateTodo;
using Luftborn.Server.Core.Application.Todos.Commands.DeleteTodo;
using Luftborn.Server.Core.Application.Todos.Commands.UpdateTodo;
using Luftborn.Server.Core.Application.Todos.Queries.GetTodo;
using Luftborn.Server.Core.Application.Todos.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Luftborn.Server.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(Guid id)
    {
        var todo = await _mediator.Send(new GetTodoQuery(id));

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoCommand command)
    {
        var todo = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoDto>> UpdateTodo([FromRoute]Guid id, [FromBody] UpdateTodoCommand command)
    {
        command.Id = id;

        var todo = await _mediator.Send(command);
        if(todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo([FromRoute]Guid id)
    {
        var result = await _mediator.Send(new DeleteTodoCommand(id));

        if (!result)
            return NotFound();

        return NoContent();
    }
} 