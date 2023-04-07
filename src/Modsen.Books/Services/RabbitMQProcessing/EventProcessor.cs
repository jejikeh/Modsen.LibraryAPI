using System.Text.Json;
using AutoMapper;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Services.RabbitMQProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventProcessor> _logger;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
        _logger = logger;
    }
    
    public void ProcessEvent(string message)
    {
        _logger.LogInformation("--> Determining Event");
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.AuthorPublished:
                AddAuthor(message);
                break;
            case EventType.AuthorDeleted:
                DeleteAuthor(message);
                break;
            case EventType.AuthorUpdated:
                UpdateAuthor(message);
                break;
        }
    }

    private async void AddAuthor(string authorPublishedMessage)
    {
        _logger.LogInformation("--> Author started added process!");
        
        using var scope = _serviceScopeFactory.CreateScope();
        var authorRepository = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();
        
        var authorPublishedDto = JsonSerializer.Deserialize<AuthorPublishedDto>(authorPublishedMessage);
        var author = _mapper.Map<Author>(authorPublishedDto);
        if (await authorRepository.ExternalAuthorExist(authorPublishedDto.Id))
            return;
        
        author.Id = Guid.NewGuid();
        await authorRepository.CreateAuthor(author);
        await authorRepository.SaveChangesAsync();
        
        _logger.LogInformation("--> Author added!");
    }
    
    private async void UpdateAuthor(string authorPublishedMessage)
    {
        _logger.LogInformation("--> Author started update process!");
        
        using var scope = _serviceScopeFactory.CreateScope();
        var authorRepository = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();
        
        var authorDto = JsonSerializer.Deserialize<AuthorPublishedDto>(authorPublishedMessage);
        var author = _mapper.Map<Author>(authorDto);
        if (!await authorRepository.ExternalAuthorExist(author.Id))
            return;
        
        await authorRepository.UpdateAuthor(author);
        await authorRepository.SaveChangesAsync();
        
        _logger.LogInformation("--> Author updated!");
    }
    
    private async void DeleteAuthor(string authorPublishedMessage)
    {
        _logger.LogInformation("--> Author started delete process!");
        
        using var scope = _serviceScopeFactory.CreateScope();
        var authorRepository = scope.ServiceProvider.GetRequiredService<IAuthorRepository>();
        
        var authorDeletedId = JsonSerializer.Deserialize<AuthorDeleteDto>(authorPublishedMessage);
        if (!await authorRepository.ExternalAuthorExist(authorDeletedId.Id))
            return;
        
        await authorRepository.DeleteAuthorByExternalId(authorDeletedId.Id);
        await authorRepository.SaveChangesAsync();
        
        _logger.LogInformation("--> Author deleted!");
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        return eventType?.Event switch
        {
            "Author_Published" => EventType.AuthorPublished,
            "Author_Updated" => EventType.AuthorUpdated,
            "Author_Deleted" => EventType.AuthorDeleted,
            _ => EventType.Undetermined
        };
    }
}

enum EventType
{
    AuthorPublished,
    AuthorUpdated,
    AuthorDeleted,
    Undetermined
}