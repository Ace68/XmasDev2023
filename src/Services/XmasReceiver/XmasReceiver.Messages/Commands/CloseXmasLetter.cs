using Muflone.Messages.Commands;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.Commands;

public sealed class CloseXmasLetter(XmasLetterId aggregateId, Guid commitId)
	: Command(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}