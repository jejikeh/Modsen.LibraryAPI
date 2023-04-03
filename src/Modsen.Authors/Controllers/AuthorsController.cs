using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Authors.Application.Commands.CreateAuthor;
using Modsen.Authors.Application.Commands.GetAllAuthors;
using Modsen.Authors.Application.Interfaces;

namespace Modsen.Authors.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAuthorCommand createAuthorCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        await Mediator.Send(createAuthorCommand);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorReadMinDto>>> GetAllAuthorsMin()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAllAuthorsMinQuery());
        return Ok(authors);
    }
}