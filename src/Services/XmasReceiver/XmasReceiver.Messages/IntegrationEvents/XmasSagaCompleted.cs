using Muflone.Messages.Events;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.IntegrationEvents;

public sealed class XmasSagaCompleted(XmasLetterId aggregateId, Guid correlationId)
	: IntegrationEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}