using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasLogistics.Messages.Events;

namespace XmasLogistics.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsReadyToSendConsumer(IServiceProvider serviceProvider,
		IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: IntegrationEventsConsumerBase<XmasPresentsReadyToSend>(mufloneConnectionFactory, loggerFactory)
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<XmasPresentsReadyToSend>> HandlersAsync { get; } =
		ServiceProviderServiceExtensions.GetServices<IIntegrationEventHandlerAsync<XmasPresentsReadyToSend>>(serviceProvider);
}