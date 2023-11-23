using Muflone.Messages.Events;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Messages.Events;

public sealed class XmasPresentsPrepared(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody)
	: DomainEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}