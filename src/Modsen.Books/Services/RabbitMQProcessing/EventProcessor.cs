using System.Text.Json;
using AutoMapper;
using MediatR;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Services.RabbitMQProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventProcessor> _logger;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, IMediator mediator, ILogger<EventProcessor> logger)
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
            case EventType.PlatformPublished:
                AddAuthor(message);
                break;
        }
    }

    private async void AddAuthor(string authorPublishedMessage)
    {
        _logger.LogInformation("--> Author started added process!");
        using var scope = _serviceScopeFactory.CreateScope();
        var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
        var authorPublishedDto = JsonSerializer.Deserialize<AuthorPublishedDto>(authorPublishedMessage);
        var author = _mapper.Map<Author>(authorPublishedDto);
        if (await bookRepository.ExternalAuthorExist(author.ExternalId))
            return;
        
        author.Id = Guid.NewGuid();
        await bookRepository.CreateAuthor(author);
        await bookRepository.SaveChangesAsync();
        
        _logger.LogInformation("--> Author added!");
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
        return eventType?.Event switch
        {
            "Author_Published" => EventType.PlatformPublished,
            _ => EventType.Undetermined
        };
    }
}

enum EventType
{
    PlatformPublished,
    Undetermined
}