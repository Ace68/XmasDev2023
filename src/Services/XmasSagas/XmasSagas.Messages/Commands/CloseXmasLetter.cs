using Muflone.Messages.Commands;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.Commands;

public sealed class CloseXmasLetter(XmasLetterId aggregateId, Guid commitId) : Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}