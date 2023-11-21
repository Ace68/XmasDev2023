using Muflone.Messages.Commands;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.Commands;

public sealed class ReceiveXmasLetter : Command
{
	public readonly XmasLetterId XmasLetterId;
	public readonly XmasLetterNumber XmasLetterNumber;

	public readonly ReceivedOn ReceivedOn;
	public readonly ChildEmail ChildEmail;
	public readonly LetterSubject LetterSubject;
	public readonly LetterBody LetterBody;

	public ReceiveXmasLetter(XmasLetterId aggregateId, Guid commitId, XmasLetterNumber xmasLetterNumber,
		ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject,
		LetterBody letterBody) : base(aggregateId, commitId)
	{
		XmasLetterId = aggregateId;
		XmasLetterNumber = xmasLetterNumber;
		ReceivedOn = receivedOn;
		ChildEmail = childEmail;
		LetterSubject = letterSubject;
		LetterBody = letterBody;
	}
}