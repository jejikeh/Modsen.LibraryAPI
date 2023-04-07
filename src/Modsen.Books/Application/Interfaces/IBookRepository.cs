﻿using Modsen.Books.Models;

namespace Modsen.Books.Application.Interfaces;

public interface IBookRepository
{
    // Author related
    public Task<IEnumerable<Author>> GetAllAuthors();
    public Task<Author> CreateAuthor(Author author);
    public Task<bool> AuthorExist(Guid authorId);
    public Task<bool> ExternalAuthorExist(Guid externalAuthorId);
    
    // Book related
    public Task<IEnumerable<Book>> GetAllBooks();
    public IEnumerable<Book> GetAllAuthorBooks(Guid authorId);
    public Task<Book?> GetBookById(Guid authorId, Guid bookId);
    public Task CreateBook(Guid authorId, Book book);
    public Task<bool> SaveChangesAsync();
}