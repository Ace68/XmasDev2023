using Muflone.Messages.Events;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.DomainEvents;

public sealed class XmasLetterClosed(XmasLetterId aggregateId, Guid commitId)
	: DomainEvent(aggregateId, commitId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}