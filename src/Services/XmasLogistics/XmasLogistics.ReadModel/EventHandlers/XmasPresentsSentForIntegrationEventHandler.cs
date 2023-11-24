using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using XmasLogistics.Messages.Events;

namespace XmasLogistics.ReadModel.EventHandlers;

public sealed class XmasPresentsSentForIntegrationEventHandler : DomainEventHandlerAsync<XmasPresentsSent>
{
	private readonly IEventBus _eventBus;

	public XmasPresentsSentForIntegrationEventHandler(ILoggerFactory loggerFactory, IEventBus eventBus) : base(loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task HandleAsync(XmasPresentsSent @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await _eventBus.PublishAsync(new XmasLetterProcessed(@event.XmasLetterId, correlationId, @event.LetterBody), cancellationToken);
	}
}