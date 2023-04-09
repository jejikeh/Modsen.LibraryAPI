using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Authors.Application.Commands.CreateAuthor;
using Modsen.Authors.Application.Commands.DeleteAuthor;
using Modsen.Authors.Application.Commands.GetAuthor;
using Modsen.Authors.Application.Commands.GetAuthors;
using Modsen.Authors.Application.Commands.UpdateAuthor;
using Modsen.Authors.Application.Dtos;
using Modsen.Authors.Services.RabbitMQ;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Authors.Controllers;

/// <summary>
/// Author service endpoints
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMessageBusClient _messageBusClient;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public AuthorsController(IMapper mapper, IMessageBusClient messageBusClient)
    {
        _mapper = mapper;
        _messageBusClient = messageBusClient;
    }
    
    [SwaggerOperation(Summary = "Get all authors")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDetailsDto>>> GetAllAuthors()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAuthorsQuery());
        return Ok(authors);
    }
    
    [SwaggerOperation(Summary = "Get detail information about author")]
    [HttpGet("{id:guid}")]
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
    
    [SwaggerOperation(Summary = "Create author")]
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
        
        return Ok(authorDto);
    }
    
    [SwaggerOperation(Summary = "Update existing author")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AuthorDetailsDto>> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDto updateAuthorDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var updateAuthorCommand = new UpdateAuthorCommand()
        {
            Id = id,
            Bio = updateAuthorDto.Bio,
            Born = updateAuthorDto.Born,
            Die = updateAuthorDto.Die,
            FirstName = updateAuthorDto.FirstName,
            LastName = updateAuthorDto.LastName
        };
        
        var updatedAuthor = await Mediator.Send(updateAuthorCommand);
        var platformPublishDto = _mapper.Map<AuthorPublishDto>(updatedAuthor);
        platformPublishDto.Event = "Author_Updated";
        _messageBusClient.PublishUpdateAuthor(platformPublishDto);

        return Ok(updatedAuthor);
    }
    
    [SwaggerOperation(Summary = "Delete author by Id")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<AuthorDetailsDto>> DeleteAuthor(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var deleteAuthorCommand = new DeleteAuthorCommand()
        {
            Id = id
        };
        
        await Mediator.Send(deleteAuthorCommand);
        var platformPublishDto = _mapper.Map<AuthorDeleteDto>(deleteAuthorCommand);
        platformPublishDto.Event = "Author_Deleted";
        _messageBusClient.PublishDeleteAuthor(platformPublishDto);

        return Ok(deleteAuthorCommand);
    }
}