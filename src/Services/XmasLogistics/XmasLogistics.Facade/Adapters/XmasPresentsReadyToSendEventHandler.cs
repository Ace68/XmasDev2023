using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using XmasLogistics.Messages.Commands;
using XmasLogistics.Messages.Events;

namespace XmasLogistics.Facade.Adapters;

public sealed class XmasPresentsReadyToSendEventHandler
	(ILoggerFactory loggerFactory, IServiceBus serviceBus) : IntegrationEventHandlerAsync<XmasPresentsReadyToSend>(loggerFactory)
{
	public override async Task HandleAsync(XmasPresentsReadyToSend @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		@event.UserProperties.TryGetValue("SagaState", out var sagaState);

		var sendXmasPresents = new SendXmasPresents(@event.XmasLetterId, correlationId, @event.LetterBody);
		sendXmasPresents.UserProperties.Add("SagaState", sagaState);
		await serviceBus.SendAsync(sendXmasPresents, cancellationToken);
	}
}