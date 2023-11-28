using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.IntegrationEvents;

namespace XmasSagas.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsApprovedConsumer(IServiceProvider serviceProvider, IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: SagaEventConsumerBase<XmasPresentsApproved>(mufloneConnectionFactory, loggerFactory)
{
	protected override ISagaEventHandlerAsync<XmasPresentsApproved> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaEventHandlerAsync<XmasPresentsApproved>>();
}