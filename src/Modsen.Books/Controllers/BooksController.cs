using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.CreateBook;
using Modsen.Books.Application.Commands.GetBook;
using Modsen.Books.Application.Commands.GetBooks;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public BooksController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookMinDetailsDto>>> GetAllBooksMin()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var books = await Mediator.Send(new GetBooksMinQuery());
        return Ok(books);
    }
    
    [HttpGet]
    public async Task<ActionResult<BookDetailsDto>> GetBookDetails([FromBody] GetBookDetailsQuery bookDetailsQuery)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(bookDetailsQuery));
    }
    
    [HttpPost]
    public async Task<ActionResult<BookDetailsDto>> Create([FromBody] CreateBookCommand createBookCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var book = await Mediator.Send(createBookCommand);
        return Ok(_mapper.Map<BookDetailsDto>(book));
    }
}