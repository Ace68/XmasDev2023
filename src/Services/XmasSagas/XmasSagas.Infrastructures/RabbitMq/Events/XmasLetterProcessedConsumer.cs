using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.IntegrationEvents;

namespace XmasSagas.Infrastructures.RabbitMq.Events;

public sealed class XmasLetterProcessedConsumer(IServiceProvider serviceProvider, IMufloneConnectionFactory mufloneConnectionFactory,
	ILoggerFactory loggerFactory) : SagaEventConsumerBase<XmasLetterProcessed>(mufloneConnectionFactory, loggerFactory)
{
	protected override ISagaEventHandlerAsync<XmasLetterProcessed> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaEventHandlerAsync<XmasLetterProcessed>>();
}