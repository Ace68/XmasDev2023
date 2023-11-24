using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Models;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.Commands;
using XmasSagas.Orchestrators.Sagas;

namespace XmasSagas.Infrastructures.RabbitMq.Commands;

public class StartXmasLetterSagaConsumer(IServiceBus serviceBus, ISagaRepository sagaRepository,
		ConsumerConfiguration configuration, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: SagaStartedByConsumerBase<StartXmasLetterSaga>(configuration, connectionFactory, loggerFactory)
{
	protected override ISagaStartedByAsync<StartXmasLetterSaga> HandlerAsync { get; } = new XmasLetterSaga(serviceBus, sagaRepository, loggerFactory);
}