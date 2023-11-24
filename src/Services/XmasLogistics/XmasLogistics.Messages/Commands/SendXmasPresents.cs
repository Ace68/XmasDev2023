using Muflone.Messages.Commands;
using XmasLogistics.Shared.CustomTypes;
using XmasLogistics.Shared.DomainIds;

namespace XmasLogistics.Messages.Commands;

public sealed class SendXmasPresents(XmasLetterId aggregateId, Guid commitId, LetterBody letterBody)
	: Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}