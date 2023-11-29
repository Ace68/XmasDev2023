using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasWarehouses.Messages.Events;

namespace XmasWarehouses.ReadModel.EventHandlers;

public sealed class XmasPresentsPreparedForIntegrationEventHandler
	(ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<XmasPresentsPrepared>(loggerFactory)
{
	public override async Task HandleAsync(XmasPresentsPrepared @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		@event.UserProperties.TryGetValue("SagaState", out var sagaState);
		var xmasPresentsReadyToSend = new XmasPresentsReadyToSend(@event.XmasLetterId, correlationId, @event.LetterBody);
		xmasPresentsReadyToSend.UserProperties.Add("SagaState", sagaState);

		await eventBus.PublishAsync(xmasPresentsReadyToSend, cancellationToken);
	}
}