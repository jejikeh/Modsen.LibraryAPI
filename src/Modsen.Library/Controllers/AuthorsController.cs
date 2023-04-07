using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Modsen.Library.Application.Commands.CreateUser;
using Modsen.Library.Application.Commands.GetUser;
using Modsen.Library.Application.Commands.GetUsers;
using Modsen.Library.Application.Dtos;
using Modsen.Library.Configuration;
using Modsen.Library.Models;
using Modsen.Library.Services.DataClient;

namespace Modsen.Library.Controllers;

/// <summary>
/// Endpoint Wrapper to internal modsen-authors service
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private readonly IAuthorDataClient _authorDataClient;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public AuthorsController(IMapper mapper, IMediator? mediator, IAuthorDataClient authorDataClient)
    {
        _mapper = mapper;
        _mediator = mediator;
        _authorDataClient = authorDataClient;
    }

    /// <summary>
    /// Fetch authors data from the author service
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        var authors = await _authorDataClient.GetAllAuthors();
        return Ok(authors);
    }
}