using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.IntegrationEvents;

namespace XmasSagas.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsReadyToSendConsumer(IServiceProvider serviceProvider, IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: SagaEventConsumerBase<XmasPresentsReadyToSend>(mufloneConnectionFactory, loggerFactory)
{
	protected override ISagaEventHandlerAsync<XmasPresentsReadyToSend> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaEventHandlerAsync<XmasPresentsReadyToSend>>();
}