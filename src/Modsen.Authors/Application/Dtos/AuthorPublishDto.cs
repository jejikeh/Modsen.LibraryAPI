using AutoMapper;
using Modsen.Authors.Application.Common.Mappings;

namespace Modsen.Authors.Application.Dtos;

public class AuthorPublishDto : IMapWith<AuthorDetailsDto>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Event { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuthorDetailsDto, AuthorPublishDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(authorsDto => authorsDto.Id))
            .ForMember(
                dto => dto.Name,
                expression => expression.MapFrom(authorsDto => $"{authorsDto.FirstName} {authorsDto.LastName}"));
    }
}