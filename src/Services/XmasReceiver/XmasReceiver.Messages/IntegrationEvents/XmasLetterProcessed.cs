using Muflone.Messages.Events;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.IntegrationEvents;

public sealed class XmasLetterProcessed
	(XmasLetterId aggregateId, Guid correlationId) : IntegrationEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly Guid CorrelationId = correlationId;
}