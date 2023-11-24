using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasLogistics.Messages.Events;
using XmasLogistics.ReadModel.EventHandlers;

namespace XmasLogistics.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsSentConsumer(IEventBus eventbus, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<XmasPresentsSent>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasPresentsSent>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasPresentsSent>>
	{
		new XmasPresentsSentForIntegrationEventHandler(loggerFactory, eventbus)
	};
}