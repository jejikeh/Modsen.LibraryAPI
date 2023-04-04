using AutoMapper;
using Modsen.Authors.Application.Commands.GetAllAuthors;
using Modsen.Authors.Application.Common.Mappings;
using Modsen.Authors.Models;

namespace Modsen.Authors.Application.Commands.GetAuthor;

public class AuthorDetailsDto : IMapWith<Author>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Born { get; set; }
    public DateTime? Die { get; set; }
    public string? Bio { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Author, AuthorDetailsDto>()
            .ForMember(
                dto => dto.FirstName,
                expression => expression.MapFrom(author => author.FirstName))
            .ForMember(
                dto => dto.LastName,
                expression => expression.MapFrom(author => author.LastName))
            .ForMember(
                dto => dto.Bio,
                expression => expression.MapFrom(author => author.Bio))
            .ForMember(
                dto => dto.Born,
                expression => expression.MapFrom(author => author.Born))
            .ForMember(
                dto => dto.Die,
                expression => expression.MapFrom(author => author.Die));
    }
}