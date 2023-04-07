using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Authors.Application.Commands.CreateAuthor;
using Modsen.Authors.Application.Commands.GetAuthor;
using Modsen.Authors.Application.Commands.GetAuthors;
using Modsen.Authors.Application.Dtos;
using Modsen.Authors.Services.RabbitMQ;

namespace Modsen.Authors.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMessageBusClient _messageBusClient;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public AuthorsController(IMapper mapper, IMessageBusClient messageBusClient)
    {
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDetailsDto>>> GetAllAuthorMin()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAuthorsQuery());
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
    public async Task<ActionResult<AuthorDetailsDto>> CreateAuthor([FromBody] CreateAuthorCommand createAuthorCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var author = await Mediator.Send(createAuthorCommand);
        var authorDto = _mapper.Map<AuthorDetailsDto>(author);

        var platformPublishDto = _mapper.Map<AuthorPublishDto>(authorDto);
        platformPublishDto.Event = "Author_Published";
        _messageBusClient.PublishNewAuthor(platformPublishDto);
        
        return Ok(_mapper.Map<AuthorDetailsDto>(author));
    }
}