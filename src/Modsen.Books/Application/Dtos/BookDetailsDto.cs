﻿using AutoMapper;
using Modsen.Books.Application.Common.Mappings;
using Modsen.Books.Models;

namespace Modsen.Books.Application.Dtos;

public class BookDetailsDto : IMapWith<Book>
{
    public required Guid Id { get; set; }
    public required string ISBN { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required string Description { get; set; }
    public required DateTime Year { get; set; }
    public required Guid AuthorId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookDetailsDto>()
            .ForMember(
                dto => dto.Id,
                expression => expression.MapFrom(book => book.Id))
            .ForMember(
                dto => dto.ISBN,
                expression => expression.MapFrom(book => book.ISBN))
            .ForMember(
                dto => dto.Title,
                expression => expression.MapFrom(book => book.Title))
            .ForMember(
                dto => dto.Genre,
                expression => expression.MapFrom(book => book.Genre))
            .ForMember(
                dto => dto.Description,
                expression => expression.MapFrom(book => book.Description))
            .ForMember(
                dto => dto.Year,
                expression => expression.MapFrom(book => book.Year))
            .ForMember(
                dto => dto.AuthorId,
                expression => expression.MapFrom(book => book.AuthorId));
    }
}