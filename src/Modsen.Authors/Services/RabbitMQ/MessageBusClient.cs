﻿using System.Text;
using System.Text.Json;
using Modsen.Authors.Application.Dtos;
using RabbitMQ.Client;

namespace Modsen.Authors.Services.RabbitMQ;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MessageBusClient> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private string _exchange = "trigger";

    public MessageBusClient(IConfiguration configuration, ILogger<MessageBusClient> logger)
    {
        _configuration = configuration;
        _logger = logger;
        var uri = _configuration.GetServiceUri("rabbit");
        _logger.LogInformation($"RabbitMQ initialization on {uri.Host}");
        var endpoint = new AmqpTcpEndpoint(uri);
        var factory = new ConnectionFactory()
        {
            Endpoint = endpoint,
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchange, ExchangeType.Fanout);
            _connection.ConnectionShutdown += RabbitMqConnectionShutdown;
            
            _logger.LogInformation("Connected to message bus");
        }
        catch (Exception exception)
        {
            _logger.LogError($"Could not connect to message bus {exception.Message}");
        }
    }

    public void PublishNewAuthor(AuthorPublishDto authorPublishDto)
    {
        var message = JsonSerializer.Serialize(authorPublishDto);

        if (_connection.IsOpen)
        {
            _logger.LogInformation("Pushing create actorPublishDto to the message bus...");
            SendMessage(message);
        }
        else
        {
            _logger.LogError("Unable to push, RabbitMq connection is closed.");
        }
    }

    public void PublishUpdateAuthor(AuthorPublishDto authorPublishDto)
    {
        var message = JsonSerializer.Serialize(authorPublishDto);

        if (_connection.IsOpen)
        {
            _logger.LogInformation("Pushing update actorPublishDto to the message bus...");
            SendMessage(message);
        }
        else
        {
            _logger.LogError("Unable to push, RabbitMq connection is closed.");
        }
    }

    public void PublishDeleteAuthor(AuthorDeleteDto authorDeleteDto)
    {
        var message = JsonSerializer.Serialize(authorDeleteDto);

        if (_connection.IsOpen)
        {
            _logger.LogInformation("Pushing author delete id to the message bus...");
            SendMessage(message);
        }
        else
            _logger.LogError("Unable to push, RabbitMq connection is closed.");
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(
            _exchange,
            routingKey: "",
            body: body);
        
        _logger.LogInformation($"Message sent: {message}");
    }
    
    private void RabbitMqConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("RabbitMQ connection shutdown");
    }

    public void Dispose()
    {
        _logger.LogInformation("Message bus disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        
        _connection.Dispose();
        _channel.Dispose();
    }
}