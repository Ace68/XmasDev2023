using Muflone.Messages.Events;
using XmasLogistics.Shared.CustomTypes;
using XmasLogistics.Shared.DomainIds;

namespace XmasLogistics.Messages.Events;

public sealed class XmasPresentsSent(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody) : DomainEvent(aggregateId, correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}