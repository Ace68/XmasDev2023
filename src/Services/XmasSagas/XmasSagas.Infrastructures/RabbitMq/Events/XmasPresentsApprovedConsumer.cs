using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.IntegrationEvents;
using XmasSagas.Orchestrators.Sagas;

namespace XmasSagas.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsApprovedConsumer(IServiceBus serviceBus, ISagaRepository sagaRepository,
		IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory)
	: SagaEventConsumerBase<XmasPresentsApproved>(mufloneConnectionFactory, loggerFactory)
{
	protected override ISagaEventHandlerAsync<XmasPresentsApproved> HandlerAsync { get; } = new XmasLetterSaga(serviceBus, sagaRepository, loggerFactory);
}