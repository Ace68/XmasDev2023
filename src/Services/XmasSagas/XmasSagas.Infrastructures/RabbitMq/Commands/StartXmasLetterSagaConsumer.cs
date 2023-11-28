using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Models;
using Muflone.Transport.RabbitMQ.Saga.Consumers;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.RabbitMq.Commands;

public class StartXmasLetterSagaConsumer(IServiceProvider serviceProvider, ConsumerConfiguration configuration, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: SagaStartedByConsumerBase<StartXmasLetterSaga>(configuration, connectionFactory, loggerFactory)
{
	protected override ISagaStartedByAsync<StartXmasLetterSaga> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaStartedByAsync<StartXmasLetterSaga>>();
}