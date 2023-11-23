using Muflone.Messages.Commands;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Messages.Commands;

public sealed class PrepareXmasPresents(XmasLetterId aggregateId, Guid commitId, LetterBody letterBody)
	: Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}