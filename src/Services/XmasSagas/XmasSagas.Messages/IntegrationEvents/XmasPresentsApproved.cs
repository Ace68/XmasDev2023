using Muflone.Messages.Events;
using XmasSagas.Shared.CustomTypes;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Messages.IntegrationEvents;

public sealed class XmasPresentsApproved
	(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody) : IntegrationEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}