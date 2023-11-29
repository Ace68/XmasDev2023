using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasReceiver.Messages.IntegrationEvents;

namespace XmasReceiver.Infrastructures.RabbitMq.Events;

public sealed class XmasLetterProcessedConsumer(IServiceProvider serviceProvider,
		IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: IntegrationEventsConsumerBase<XmasLetterProcessed>(mufloneConnectionFactory, loggerFactory)
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<XmasLetterProcessed>> HandlersAsync { get; } =
		ServiceProviderServiceExtensions.GetServices<IIntegrationEventHandlerAsync<XmasLetterProcessed>>(serviceProvider);
}