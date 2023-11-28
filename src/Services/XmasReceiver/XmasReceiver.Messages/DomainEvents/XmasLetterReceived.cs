using Muflone.Messages.Events;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.Messages.DomainEvents;

public sealed class XmasLetterReceived(XmasLetterId aggregateId, Guid commitId, XmasLetterNumber xmasLetterNumber,
		ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject,
		LetterBody letterBody, XmasLetterStatus xmasLetterStatus)
	: DomainEvent(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly XmasLetterNumber XmasLetterNumber = xmasLetterNumber;

	public readonly ReceivedOn ReceivedOn = receivedOn;
	public readonly ChildEmail ChildEmail = childEmail;

	public readonly LetterSubject LetterSubject = letterSubject;
	public readonly LetterBody LetterBody = letterBody;

	public readonly XmasLetterStatus XmasLetterStatus = xmasLetterStatus;
}