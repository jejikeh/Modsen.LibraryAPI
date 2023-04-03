using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modsen.Authors.Application.Interfaces;

namespace Modsen.Authors.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;


    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
}