using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.GetAuthors;
using Modsen.Books.Application.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Books.Controllers;

[Route("api/[controller]")]
[SwaggerTag("Authors controller")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    
    public AuthorsController(ILogger<AuthorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDetailsDto>>> GetAuthors()
    {
        _logger.LogInformation("Getting authors from book-service");
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        return Ok(await Mediator.Send(new GetAuthorsQuery()));
    }
}