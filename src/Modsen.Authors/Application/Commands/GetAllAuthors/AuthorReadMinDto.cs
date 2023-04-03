using AutoMapper;
using Modsen.Authors.Application.Common.Mappings;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.GetAllAuthors;

public class AuthorReadMinDto : IMapWith<Author>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorReadMinDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(author => author.Id))
            .ForMember(
                dto => dto.Name,
                expression => expression.MapFrom(author => 
                    $"{author.FirstName} {author.LastName}"));
    }
}