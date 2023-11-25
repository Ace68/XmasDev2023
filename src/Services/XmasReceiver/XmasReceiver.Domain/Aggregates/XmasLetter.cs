using Muflone.Core;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.Domain.Aggregates;

public class XmasLetter : AggregateRoot
{
	private XmasLetterId _xmasLetterId;
	private XmasLetterNumber _xmasLetterNumber;

	private ReceivedOn _receivedOn;
	private ChildEmail _childEmail;
	private LetterSubject _letterSubject;
	private LetterBody _letterBody;

	private XmasLetterStatus _xmasletterStatus;

	protected XmasLetter()
	{
	}

	internal static XmasLetter ReceiveXmasLetter(XmasLetterId xmasLetterId, Guid correlationId,
		XmasLetterNumber xmasLetterNumber, ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject,
		LetterBody letterBody)
	{
		var xmasLetter = new XmasLetter(xmasLetterId, correlationId, xmasLetterNumber, receivedOn, childEmail, letterSubject, letterBody);
		return xmasLetter;
	}

	private XmasLetter(XmasLetterId xmasLetterId, Guid correlationId, XmasLetterNumber xmasLetterNumber,
		ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject, LetterBody letterBody)
	{
		RaiseEvent(new XmasLetterReceived(xmasLetterId, correlationId, xmasLetterNumber, receivedOn, childEmail,
			letterSubject, letterBody, XmasLetterStatus.Received));
	}

	private void Apply(XmasLetterReceived @event)
	{
		Id = @event.XmasLetterId;

		_xmasLetterId = @event.XmasLetterId;
		_xmasLetterNumber = @event.XmasLetterNumber;

		_receivedOn = @event.ReceivedOn;
		_childEmail = @event.ChildEmail;

		_letterSubject = @event.LetterSubject;
		_letterBody = @event.LetterBody;
	}

	internal void CloseXmasLetter(Guid correlationId)
	{
		RaiseEvent(new XmasLetterClosed(_xmasLetterId, correlationId));
	}

	private void Apply(XmasLetterClosed @event)
	{
		_xmasletterStatus = XmasLetterStatus.Processed;
	}
}