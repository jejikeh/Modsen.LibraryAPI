using AutoMapper;
using Modsen.Books.Application.Common.Mappings;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Dtos;

public class BookMinDetailsDto : IMapWith<Book>
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookMinDetailsDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(book => book.Id))
            .ForMember(
                dto => dto.Title,
                expression => expression.MapFrom(book => book.Title))
            .ForMember(
                dto => dto.Author,
                expression => expression.MapFrom(book => book.Author));
    }
}