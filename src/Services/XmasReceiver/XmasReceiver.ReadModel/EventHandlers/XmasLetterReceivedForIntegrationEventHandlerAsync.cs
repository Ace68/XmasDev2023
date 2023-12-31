﻿using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.Messages.IntegrationEvents;

namespace XmasReceiver.ReadModel.EventHandlers;

public sealed class XmasLetterReceivedForIntegrationEventHandlerAsync
	(ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<XmasLetterReceived>(loggerFactory)
{
	public override async Task HandleAsync(XmasLetterReceived @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await eventBus.PublishAsync(new XmasPresentsApproved(@event.XmasLetterId, correlationId, @event.LetterBody), cancellationToken);
	}
}