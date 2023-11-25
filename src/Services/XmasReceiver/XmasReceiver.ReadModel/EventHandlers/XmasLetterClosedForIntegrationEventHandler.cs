using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.Messages.IntegrationEvents;

namespace XmasReceiver.ReadModel.EventHandlers;

public sealed class XmasLetterClosedForIntegrationEventHandler
	(ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<XmasLetterClosed>(loggerFactory)
{
	public override async Task HandleAsync(XmasLetterClosed @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await eventBus.PublishAsync(new XmasSagaCompleted(@event.XmasLetterId, correlationId), cancellationToken);
	}
}