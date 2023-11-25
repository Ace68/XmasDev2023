﻿using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.ReadModel.EventHandlers;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.Infrastructures.RabbitMq.Events;

public sealed class XmasLetterClosedConsumer(IXmasLetterService xmasLetterService, IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<XmasLetterClosed>(connectionFactory,
	loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasLetterClosed>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasLetterClosed>>
	{
		new XmasLetterClosedEventHandler(loggerFactory, xmasLetterService),
		new XmasLetterClosedForIntegrationEventHandler(loggerFactory, eventBus)
	};
}