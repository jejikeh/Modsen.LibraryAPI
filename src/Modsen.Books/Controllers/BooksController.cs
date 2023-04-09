using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.GetBookByISBN;
using Modsen.Books.Application.Commands.GetBooks;
using Modsen.Books.Application.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Books.Controllers;


[Route("api/[controller]")]
[SwaggerTag("Books controller")]
[ApiController]
public class BooksController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [SwaggerOperation("Get all books")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDetailsDto>>> GetAllBooks()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetBooksQuery()));
    }
    
    [SwaggerOperation("Get book by isbn")]
    [HttpGet("{isbn}")]
    public async Task<ActionResult<BookDetailsDto>> GetBookByISBN(string isbn)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetBookByISBNQuery()
        {
            ISBN = isbn
        }));
    }
}