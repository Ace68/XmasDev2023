using Muflone.Messages.Events;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.IntegrationEvents;

public sealed class XmasPresentsApproved : IntegrationEvent
{
	public readonly XmasLetterId XmasLetterId;
	public readonly LetterBody LetterBody;

	public XmasPresentsApproved(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody) : base(aggregateId,
		correlationId)
	{
		XmasLetterId = aggregateId;
		LetterBody = letterBody;
	}
}