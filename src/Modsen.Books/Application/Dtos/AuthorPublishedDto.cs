using AutoMapper;
using Modsen.Books.Application.Common.Mappings;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Dtos;

public class AuthorPublishedDto : IMapWith<Author>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Event { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuthorPublishedDto, Author>()
            .ForMember(
                dto => dto.ExternalId,
                expression => expression.MapFrom(authorsDto => authorsDto.Id))
            .ForMember(
                dto => dto.Name,
                expression => expression.MapFrom(authorsDto => authorsDto.Name));
    }
}