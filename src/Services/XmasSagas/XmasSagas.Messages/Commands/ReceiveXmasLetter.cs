using Muflone.Messages.Commands;
using XmasSagas.Shared.CustomTypes;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.Commands;

public sealed class ReceiveXmasLetter(XmasLetterId aggregateId, Guid commitId, XmasLetterNumber xmasLetterNumber,
		ReceivedOn receivedOn, ChildEmail childEmail, LetterSubject letterSubject,
		LetterBody letterBody)
	: Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly XmasLetterNumber XmasLetterNumber = xmasLetterNumber;

	public readonly ReceivedOn ReceivedOn = receivedOn;
	public readonly ChildEmail ChildEmail = childEmail;
	public readonly LetterSubject LetterSubject = letterSubject;
	public readonly LetterBody LetterBody = letterBody;
}