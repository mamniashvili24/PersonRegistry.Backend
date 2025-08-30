using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Application.Features.Person.Commands.CreatePerson;
using PersonRegistry.Domain.Models;

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

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreatePersonCommand command, [FromForm] IFormFile? image, CancellationToken cancellationToken)
    {
        using FileContainer fileContainer = GetFileContainer(image);
        command.Image = fileContainer;
        await mediator.Send(command, cancellationToken);
        
        return Ok();
    }

    private FileContainer GetFileContainer(IFormFile? file)
    {
        if (file == null)
            return null;

        string fileName = file.FileName;
        string contentType = file.ContentType;
        Stream stream = file.OpenReadStream();
        string fileExtension = Path.GetExtension(fileName);

        return new FileContainer(fileName, stream, contentType, fileExtension);
    }
}