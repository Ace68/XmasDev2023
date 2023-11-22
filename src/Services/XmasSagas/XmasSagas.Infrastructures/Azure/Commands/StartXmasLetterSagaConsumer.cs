using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.Azure.Models;
using Muflone.Transport.Azure.Saga.Consumers;
using XmasSagas.Messages.Commands;
using XmasSagas.Orchestrators.Sagas;

namespace XmasSagas.Infrastructures.Azure.Commands;

public class StartXmasLetterSagaConsumer : SagaStartedByConsumerBase<StartXmasLetterSaga>
{
	protected override ISagaStartedByAsync<StartXmasLetterSaga> HandlerAsync { get; }

	public StartXmasLetterSagaConsumer(IServiceBus serviceBus, ISagaRepository sagaRepository,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
	{
		HandlerAsync = new XmasLetterSaga(serviceBus, sagaRepository, loggerFactory);
	}
}