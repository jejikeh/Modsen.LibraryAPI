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

namespace Modsen.Authors.Controllers;

/// <summary>
/// Author service endpoints
/// </summary>
[Route("api/[controller]/[action]")]
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
    
    /// <summary>
    /// Get all authors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDetailsDto>>> GetAllAuthors()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var authors = await Mediator.Send(new GetAuthorsQuery());
        return Ok(authors);
    }
    
    /// <summary>
    /// Get detail information about author
    /// </summary>
    /// <param name="id">Id of the author</param>
    /// <returns>Author information</returns>
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
    
    /// <summary>
    /// Create and Publish Author to message bus
    /// </summary>
    /// <param name="createAuthorCommand">author information</param>
    /// <returns>Author information</returns>
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

    /// <summary>
    /// Update existing author
    /// </summary>
    /// <param name="updateAuthorCommand">Updated fields</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<AuthorDetailsDto>> UpdateAuthor([FromBody] UpdateAuthorCommand updateAuthorCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var updatedAuthor = await Mediator.Send(updateAuthorCommand);
        var platformPublishDto = _mapper.Map<AuthorPublishDto>(updatedAuthor);
        platformPublishDto.Event = "Author_Updated";
        _messageBusClient.PublishUpdateAuthor(platformPublishDto);

        return Ok(updatedAuthor);
    }

    /// <summary>
    /// Update existing author
    /// </summary>
    /// <param name="deleteAuthorCommand"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult<AuthorDetailsDto>> DeleteAuthor([FromBody] DeleteAuthorCommand deleteAuthorCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        await Mediator.Send(deleteAuthorCommand);
        var platformPublishDto = _mapper.Map<AuthorDeleteDto>(deleteAuthorCommand);
        platformPublishDto.Event = "Author_Deleted";
        _messageBusClient.PublishDeleteAuthor(platformPublishDto);

        return Ok(deleteAuthorCommand);
    }
}