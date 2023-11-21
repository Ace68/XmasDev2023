using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.ReadModel.EventHandlers;

public sealed class XmasLetterReceivedEventHandlerAsync(IXmasLetterService xmasLetterService,
		ILoggerFactory loggerFactory)
	: DomainEventHandlerAsync<XmasLetterReceived>(loggerFactory)
{
	public override async Task HandleAsync(XmasLetterReceived @event, CancellationToken cancellationToken = new())
	{
		await xmasLetterService.ReceiveLetterAsync(@event.XmasLetterId, @event.XmasLetterNumber, @event.ReceivedOn,
			@event.ChildEmail, @event.LetterSubject, @event.LetterBody, @event.XmasLetterStatus, cancellationToken);
	}
}