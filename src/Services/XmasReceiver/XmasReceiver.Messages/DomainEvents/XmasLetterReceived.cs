using Muflone.Messages.Events;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.Messages.DomainEvents;

public sealed class XmasLetterReceived : DomainEvent
{
	public readonly XmasLetterId XmasLetterId;
	public readonly XmasLetterNumber XmasLetterNumber;

	public readonly ReceivedOn ReceivedOn;
	public readonly ChildEmail ChildEmail;

	public readonly LetterSubject LetterSubject;
	public readonly LetterBody LetterBody;

	public readonly XmasLetterStatus XmasLetterStatus;

	public XmasLetterReceived(XmasLetterId aggregateId, Guid commitId, XmasLetterNumber xmasLetterNumber,
		ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject,
		LetterBody letterBody, XmasLetterStatus xmasLetterStatus) : base(aggregateId, commitId)
	{
		XmasLetterId = aggregateId;
		XmasLetterNumber = xmasLetterNumber;

		ReceivedOn = receivedOn;
		ChildEmail = childEmail;

		LetterSubject = letterSubject;
		LetterBody = letterBody;

		XmasLetterStatus = xmasLetterStatus;
	}
}