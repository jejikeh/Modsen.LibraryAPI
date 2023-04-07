using AutoMapper;
using Modsen.Authors.Application.Commands.DeleteAuthor;
using Modsen.Authors.Application.Common.Mappings;

namespace Modsen.Authors.Application.Dtos;

public class AuthorDeleteDto : IMapWith<DeleteAuthorCommand>
{
    public required Guid Id { get; set; }
    public required string Event { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteAuthorCommand, AuthorDeleteDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(authorsDto => authorsDto.Id));
    }
}