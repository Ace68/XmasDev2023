using Muflone.Messages.Events;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Messages.Events;

public sealed class XmasPresentsApproved(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody)
	: IntegrationEvent(aggregateId,
		correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}