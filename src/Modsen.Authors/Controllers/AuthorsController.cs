using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Authors.Application.Commands.CreateAuthor;
using Modsen.Authors.Application.Commands.GetAllAuthors;
using Modsen.Authors.Application.Commands.GetAuthor;

namespace Modsen.Authors.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public AuthorsController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorReadMinDto>>> GetAllAuthorMin()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAllAuthorsMinQuery());
        return Ok(authors);
    }
    
    [HttpGet("{id:guid}", Name = "GetAuthorDetails")]
    public async Task<ActionResult<AuthorDetailsDto>> GetAuthorDetails(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAuthorQuery()
        {
            Id = id
        });
        
        return Ok(authors);
    }
    
    [HttpPost]
    public async Task<ActionResult<AuthorDetailsDto>> Create([FromBody] CreateAuthorCommand createAuthorCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var author = await Mediator.Send(createAuthorCommand);
        return Ok(_mapper.Map<AuthorDetailsDto>(author));
    }
}