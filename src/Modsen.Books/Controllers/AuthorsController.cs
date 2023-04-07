using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.GetAuthors;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    
    public AuthorsController(ILogger<AuthorsController> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
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