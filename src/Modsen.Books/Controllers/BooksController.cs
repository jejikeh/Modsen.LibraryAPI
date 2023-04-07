using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modsen.Books.Application.Commands.CreateBook;
using Modsen.Books.Application.Commands.GetAuthorBook;
using Modsen.Books.Application.Commands.GetAuthorBooks;
using Modsen.Books.Application.Commands.GetBookByISBN;
using Modsen.Books.Application.Dtos;

namespace Modsen.Books.Controllers;


[Route("api/[controller]")]
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