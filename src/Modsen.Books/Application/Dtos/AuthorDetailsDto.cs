using AutoMapper;
using Modsen.Books.Application.Common.Mappings;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Dtos;

public class AuthorDetailsDto : IMapWith<Author>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorDetailsDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(author => author.Id))
            .ForMember(
                dto => dto.Name,
                expression => expression.MapFrom(author => author.Name));
    }
}