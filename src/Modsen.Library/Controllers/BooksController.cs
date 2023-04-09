using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Models;
using Modsen.Library.Services.DataClient;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Library.Controllers;

[SwaggerTag("Endpoint Wrapper to internal modsen-books service")]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private readonly IBooksDataClient _booksDataClient;

    public BooksController(IBooksDataClient booksDataClient)
    {
        _booksDataClient = booksDataClient;
    }

    [SwaggerOperation(Summary = "Fetch books data from the books service")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        var authors = await _booksDataClient.GetAllBooks();
        return Ok(authors);
    }
    
    [SwaggerOperation(Summary = "Fetch authors data from the books service")]
    [HttpGet("authors")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<AuthorMinimalDto>>> GetAllAuthors()
    {
        var authors = await _booksDataClient.GetAllBookAuthors();
        return Ok(authors);
    }
    
    [SwaggerOperation(Summary = "Create books data in the books service")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CreateBookDto>>> CreateBooks([FromBody] CreateBookDto createBookDto)
    {
        var bookDto = await _booksDataClient.CreateBook(createBookDto);
        return Ok(bookDto);
    }
    
    [SwaggerOperation(Summary = "Fetch books data by isbn from the books service")]
    [HttpGet("{isbn}")]
    [Authorize]
    public async Task<ActionResult<Book>> GetBookByISBN(string isbn)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        var authors = await _booksDataClient.GetBookByISBN(isbn);
        return Ok(authors);
    }
}