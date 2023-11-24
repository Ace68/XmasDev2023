using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.EventHandlers;

namespace XmasWarehouses.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsPreparedConsumer(IEventBus eventbus, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<XmasPresentsPrepared>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasPresentsPrepared>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasPresentsPrepared>>
	{
		new XmasPresentsPreparedForIntegrationEventHandler(loggerFactory, eventbus)
	};
}