using Muflone.Messages.Events;
using XmasLogistics.Shared.CustomTypes;
using XmasLogistics.Shared.DomainIds;

namespace XmasLogistics.Messages.Events;

public sealed class XmasLetterProcessed
	(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody) : IntegrationEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly Guid CorrelationId = correlationId;
}