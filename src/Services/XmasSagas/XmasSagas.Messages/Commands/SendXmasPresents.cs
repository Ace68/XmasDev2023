using Muflone.Messages.Commands;
using XmasSagas.Shared.CustomTypes;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.Commands;

public sealed class SendXmasPresents(XmasLetterId aggregateId, Guid commitId, LetterBody letterBody)
	: Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}