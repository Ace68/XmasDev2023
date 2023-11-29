using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasWarehouses.Messages.Events;

namespace XmasWarehouses.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsApprovedConsumer(IServiceProvider serviceProvider,
		IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: IntegrationEventsConsumerBase<XmasPresentsApproved>(mufloneConnectionFactory, loggerFactory)
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<XmasPresentsApproved>> HandlersAsync { get; } =
		ServiceProviderServiceExtensions.GetServices<IIntegrationEventHandlerAsync<XmasPresentsApproved>>(serviceProvider);
}