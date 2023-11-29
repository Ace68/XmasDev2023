using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasLogistics.Messages.Events;

namespace XmasLogistics.ReadModel.EventHandlers;

public sealed class XmasPresentsSentForIntegrationEventHandler
	(ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<XmasPresentsSent>(loggerFactory)
{
	public override async Task HandleAsync(XmasPresentsSent @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		@event.UserProperties.TryGetValue("SagaState", out var sagaState);
		var xmasLetterProcessed = new XmasLetterProcessed(@event.XmasLetterId, correlationId, @event.LetterBody);
		xmasLetterProcessed.UserProperties.Add("SagaState", sagaState);

		await eventBus.PublishAsync(xmasLetterProcessed, cancellationToken);
	}
}