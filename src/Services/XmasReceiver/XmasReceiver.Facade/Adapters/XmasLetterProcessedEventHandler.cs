using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using XmasReceiver.Messages.Commands;
using XmasReceiver.Messages.IntegrationEvents;

namespace XmasReceiver.Facade.Adapters;

public sealed class XmasLetterProcessedEventHandler
	(ILoggerFactory loggerFactory, IServiceBus serviceBus) : IntegrationEventHandlerAsync<XmasLetterProcessed>(loggerFactory)
{
	public override async Task HandleAsync(XmasLetterProcessed @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		@event.UserProperties.TryGetValue("SagaState", out var sagaState);

		var closeXmasLetter = new CloseXmasLetter(@event.XmasLetterId, correlationId);
		closeXmasLetter.UserProperties.Add("SagaState", sagaState);

		await serviceBus.SendAsync(closeXmasLetter, cancellationToken);
	}
}