using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Application.Features.Person.Commands.CreatePerson;

namespace PersonRegistry.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator mediator;

    public PersonController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Post([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}