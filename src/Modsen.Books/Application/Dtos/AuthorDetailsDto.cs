using AutoMapper;
using Modsen.Books.Application.Common.Mappings;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Dtos;

public class AuthorDetailsDto : IMapWith<Author>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BornDate { get; set; }
    public DateTime? DieDate { get; set; }
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
                dto => dto.BornDate,
                expression => expression.MapFrom(author => author.BornDate))
            .ForMember(
                dto => dto.DieDate,
                expression => expression.MapFrom(author => author.DieDate));
    }
}