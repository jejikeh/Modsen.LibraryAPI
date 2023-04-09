using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Library.Application.Commands.BookRents.GetAllBookRents;
using Modsen.Library.Application.Commands.BookRents.GetBookRent;
using Modsen.Library.Models;
using Modsen.Library.Services.DataClient;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Library.Controllers;

[SwaggerTag("Book Rents controller")]
[Route("api/[controller]")]
[ApiController]
public class BookRentsController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [SwaggerOperation(Summary = "All Book rents")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<BookRent>>> GetAllBookRents()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        var bookRents = await Mediator.Send(new GetBookRentsCommand());
        return Ok(bookRents);
    }
    
    [SwaggerOperation(Summary = "Get Book rent")]
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Author>>> GetBookRent(Guid id)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");
        
        var rent = await Mediator.Send(new GetBookRentCommand
        {
            Id = id
        });
        
        return Ok(rent);
    }
}