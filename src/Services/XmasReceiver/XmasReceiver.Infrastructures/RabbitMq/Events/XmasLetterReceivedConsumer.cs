using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.ReadModel.EventHandlers;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.Infrastructures.RabbitMq.Events;

public sealed class XmasLetterReceivedConsumer(IXmasLetterService xmasLetterService, IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<XmasLetterReceived>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasLetterReceived>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasLetterReceived>>
	{
		new XmasLetterReceivedEventHandlerAsync(xmasLetterService, loggerFactory),
		new XmasLetterReceivedForIntegrationEventHandlerAsync(loggerFactory, eventBus)
	};
}