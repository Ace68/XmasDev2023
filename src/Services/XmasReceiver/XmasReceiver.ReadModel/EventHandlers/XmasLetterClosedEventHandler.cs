using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.ReadModel.EventHandlers;

public sealed class XmasLetterClosedEventHandler(ILoggerFactory loggerFactory, IXmasLetterService xmasLetterService)
	: DomainEventHandlerAsync<XmasLetterClosed>(loggerFactory)
{
	public override async Task HandleAsync(XmasLetterClosed @event, CancellationToken cancellationToken = new())
	{
		await xmasLetterService.CloseXmasLetterAsync(@event.XmasLetterId, cancellationToken);
	}
}