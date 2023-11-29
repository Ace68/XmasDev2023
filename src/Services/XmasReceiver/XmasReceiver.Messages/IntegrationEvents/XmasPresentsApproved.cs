﻿using Muflone.Messages.Events;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Messages.IntegrationEvents;

public sealed class XmasPresentsApproved(XmasLetterId aggregateId, Guid correlationId, LetterBody letterBody)
	: IntegrationEvent(aggregateId,
	correlationId)
{
	public readonly XmasLetterId XmasLetterId = aggregateId;
	public readonly LetterBody LetterBody = letterBody;
}