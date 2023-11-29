using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using XmasWarehouses.Messages.Commands;
using XmasWarehouses.Messages.Events;

namespace XmasWarehouses.Facade.Adapters;

public sealed class XmasPresentsApprovedEventHandler
	(ILoggerFactory loggerFactory, IServiceBus serviceBus) : IntegrationEventHandlerAsync<XmasPresentsApproved>(loggerFactory)
{
	public override async Task HandleAsync(XmasPresentsApproved @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		@event.UserProperties.TryGetValue("SagaState", out var sagaState);

		var prepareXmasPresents = new PrepareXmasPresents(@event.XmasLetterId, correlationId, @event.LetterBody);
		prepareXmasPresents.UserProperties.Add("SagaState", sagaState);
		await serviceBus.SendAsync(prepareXmasPresents, cancellationToken);
	}
}