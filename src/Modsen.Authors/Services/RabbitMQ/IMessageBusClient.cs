﻿using Modsen.Authors.Application.Dtos;

namespace Modsen.Authors.Services.RabbitMQ;

public interface IMessageBusClient
{
    public void PublishNewAuthor(AuthorPublishDto authorPublishDto);
    public void PublishUpdateAuthor(AuthorPublishDto authorPublishDto);
    public void PublishDeleteAuthor(AuthorDeleteDto authorDeleteDto);
}