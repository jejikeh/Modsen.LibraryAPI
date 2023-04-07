using System.Text.Json;
using AutoMapper;
using MediatR;
using Modsen.Books.Application.Commands.CreateAuthor;
using Modsen.Books.Application.Dtos;
using Modsen.Books.Application.Interfaces;
using Modsen.Books.Models;

namespace Modsen.Books.Services.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;
    private IMediator _mediator;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper, IMediator mediator)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.PlatformPublished:
                
                break;
        }
    }

    private void AddAuthor(string authorPublishedMessage)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
        var authorPublishedDto = JsonSerializer.Deserialize<AuthorPublishedDto>(authorPublishedMessage);
        var author = _mapper.Map<Author>(authorPublishedMessage);
        _mediator.Send(new CreateAuthorRequest()
        {
            Author = author
        });
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