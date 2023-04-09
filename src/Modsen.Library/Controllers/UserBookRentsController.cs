using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Library.Application.Commands.BookRents.CreateBookRent;
using Modsen.Library.Application.Commands.BookRents.DeleteBookRent;
using Modsen.Library.Application.Commands.BookRents.GetUserBookRents;
using Modsen.Library.Application.Commands.BookRents.UpdateBookRent;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Library.Controllers;

[SwaggerTag("User book rent controller")]
[Route("api/users/{userId:guid}/[controller]")]
[ApiController]
public class BookRentsController : ControllerBase
{
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public BookRentsController(IMediator? mediator)
    {
        _mediator = mediator;
    }
    
    [SwaggerOperation(Summary = "Update user book rent")]
    [HttpPut]
    public async Task<ActionResult<string>> UpdateBookRent(Guid userId, [FromBody] UpdateBookRentDto updateBookRentDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var bookRent = await Mediator.Send(new UpdateBookRentCommand
        {
            Id = updateBookRentDto.Id,
            UserId = userId,
            EndRent = updateBookRentDto.EndRent,
            IsActive = updateBookRentDto.IsActive
        });
        
        return Ok(bookRent);
    }
    
    [SwaggerOperation(Summary = "Delete user book rent")]
    [HttpDelete]
    public async Task<ActionResult<string>> DeleteBookRent(Guid userId, [FromBody] DeleteBookRentDto deleteBookRentDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        await Mediator.Send(new DeleteBookRentCommand
        {
            Id = deleteBookRentDto.Id,
            UserId = userId,
        });
        
        return Ok();
    }
    
    [SwaggerOperation(Summary = "Get all user book rents")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<BookRent>>> GetAllUserBookRents(Guid userId)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetUserBookRentsCommand()
        {
            UserId = userId
        }));
    }

    [SwaggerOperation(Summary = "Create new book rent for user")]
    [HttpPost]
    public async Task<ActionResult<UserDetailsDto>> CreateBookRent(Guid userId, [FromBody] CreateBookRentDto createBookRentDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new CreateBookRentCommand()
        {
            BookId = createBookRentDto.BookId,
            RentLength = createBookRentDto.RentLength,
            UserId = userId
        }));
    }
}