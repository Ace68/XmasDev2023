using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using XmasReceiver.Messages.DomainEvents;

namespace XmasReceiver.ReadModel.EventHandlers;

public sealed class XmasLetterReceivedEventHandler : DomainEventHandlerAsync<XmasLetterReceived>
{
	public XmasLetterReceivedEventHandler(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
	}

	public override Task HandleAsync(XmasLetterReceived @event, CancellationToken cancellationToken = new())
	{
		throw new NotImplementedException();
	}
}