using Muflone.Messages.Commands;
using XmasSagas.Shared.CustomTypes;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.Commands;

public class StartXmasLetterSaga : Command
{
	public readonly XmasLetterId XmasLetterId;
	public readonly XmasLetterNumber XmasLetterNumber;

	public readonly ReceivedOn ReceivedOn;
	public readonly ChildEmail ChildEmail;
	public readonly LetterSubject LetterSubject;
	public readonly LetterBody LetterBody;

	public StartXmasLetterSaga(XmasLetterId aggregateId, Guid commitId, XmasLetterNumber xmasLetterNumber,
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