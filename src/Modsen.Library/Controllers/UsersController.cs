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
using Swashbuckle.AspNetCore.Annotations;

namespace Modsen.Library.Controllers;

/// <summary>
/// Controller for login, register, show info about users
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private IMediator? _mediator;
    private IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public UsersController(IMapper mapper, IMediator? mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [SwaggerOperation(Summary = "Get Information about authorize user")]
    [HttpGet("Account")]
    [Authorize]
    public async Task<ActionResult<UserDetailsDto>> Account()
    {
        var userId = HttpContext.User.FindFirstValue("UserId");
        if (Mediator is null || userId is null)
            return BadRequest("Internal server error");

        var userInfo = await Mediator.Send(new GetUserByIdCommand()
        {
            Id = Guid.Parse(userId)
        });

        return Ok(userInfo);
    }

    [SwaggerOperation(Summary = "Authorize endpoint")]
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLoginDto)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        var user = await Mediator.Send(new GetUserByNameCommand()
        {
            Name = userLoginDto.FirstName + " " + userLoginDto.LastName
        });

        if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            return BadRequest("Invalid Name or Password!");

        return Ok(GenerateToken(user));
    }
    
    [SwaggerOperation(Summary = "Get All user accounts info")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsers()
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(new GetUsersCommand()));
    }

    [SwaggerOperation(Summary = "Create new account")]
    [HttpPost("Register")]
    public async Task<ActionResult<UserDetailsDto>> Register([FromBody] CreateUserCommand createUserCommand)
    {
        if (Mediator is null)
            return BadRequest("Internal server error");

        return Ok(await Mediator.Send(createUserCommand));
    }
    
    private string GenerateToken(User modelUser)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, $"{modelUser.FirstName} {modelUser.LastName}"),
            new Claim("UserId", modelUser.Id.ToString()),
        };
        
        var credentials = new SigningCredentials(
            AuthConfiguration.GetSymmetricSecurityKeyStatic(), 
            SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            issuer: AuthConfiguration.Issuer,
            audience: AuthConfiguration.Audience,
            claims: claims,
            expires: AuthConfiguration.Expires,
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}