using Muflone.Messages.Commands;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.Commands;

public sealed class TellChildrenThatXmasSagaWasStarted(XmasLetterId aggregateId, Guid commitId) : Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}