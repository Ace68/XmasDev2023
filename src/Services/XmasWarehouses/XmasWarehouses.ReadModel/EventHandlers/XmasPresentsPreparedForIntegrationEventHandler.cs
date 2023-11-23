using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasWarehouses.Messages.Events;

namespace XmasWarehouses.ReadModel.EventHandlers;

public sealed class XmasPresentsPreparedForIntegrationEventHandler : DomainEventHandlerAsync<XmasPresentsPrepared>
{
	private readonly IEventBus _eventBus;

	public XmasPresentsPreparedForIntegrationEventHandler(ILoggerFactory loggerFactory, IEventBus eventBus) : base(loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task HandleAsync(XmasPresentsPrepared @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await _eventBus.PublishAsync(new XmasPresentsReadyToSend(@event.XmasLetterId, correlationId, @event.LetterBody), cancellationToken);
	}
}