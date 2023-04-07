using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.CreateBook;
using Modsen.Books.Application.Commands.GetAuthorBook;
using Modsen.Books.Application.Commands.GetAuthorBooks;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Controllers;


[Route("api/authors/{authorId:guid}/books")]
[ApiController]
public class AuthorBooksController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    
    public AuthorBooksController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDetailsDto>>> GetAuthorBooks(Guid authorId)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        return Ok( await Mediator.Send(new GetAuthorBooksQuery()
        {
            AuthorId = authorId
        }));
    }
    
    [HttpGet("{bookId:guid}", Name = "GetAuthorBook")]
    public async Task<ActionResult<BookDetailsDto>> GetAuthorBook(Guid authorId, Guid bookId)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetAuthorBookDetailsQuery()
        {
            BookId = bookId,
            AuthorId = authorId
        }));
    }
    
    [HttpPost]
    public async Task<ActionResult<BookDetailsDto>> CreateBook(Guid authorId, [FromBody] CreateBookDto createBookDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var book = await Mediator.Send(new CreateBookCommand()
        {
            AuthorId = authorId,
            Description = createBookDto.Description,
            Genre = createBookDto.Genre,
            ISBN = createBookDto.ISBN,
            Title = createBookDto.Genre,
            Year = createBookDto.Year
        });

        return CreatedAtRoute(nameof(GetAuthorBook), new { authorId, book.Id }, book);
    }
}