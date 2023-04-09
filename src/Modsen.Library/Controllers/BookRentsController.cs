using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modsen.Library.Models;
using Modsen.Library.Services.DataClient;
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Library.Controllers;

[SwaggerTag("Endpoint Wrapper to internal modsen-authors service")]
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsDataClient _authorsDataClient;

    public AuthorsController(IAuthorsDataClient authorsDataClient)
    {
        _authorsDataClient = authorsDataClient;
    }

    [SwaggerOperation(Summary = "Fetch authors data from the author service")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        var authors = await _authorsDataClient.GetAllAuthors();
        return Ok(authors);
    }
}