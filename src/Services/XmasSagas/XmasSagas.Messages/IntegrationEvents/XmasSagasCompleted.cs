using Muflone.Messages.Events;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.IntegrationEvents;

public sealed class XmasSagaCompleted(XmasLetterId aggregateId, Guid correlationId)
	: IntegrationEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
}