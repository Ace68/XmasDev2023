using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using Muflone.Transport.Azure.Models;
using Muflone.Transport.Azure.Saga.Consumers;
using XmasSagas.Messages.IntegrationEvents;
using XmasSagas.Orchestrators.Sagas;

namespace XmasSagas.Infrastructures.Azure.Events;

public sealed class XmasPresentsApprovedConsumer(IServiceBus serviceBus, ISagaRepository sagaRepository,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null)
	: SagaEventConsumerBase<XmasPresentsApproved>(azureServiceBusConfiguration, loggerFactory, messageSerializer)
{
	protected override ISagaEventHandlerAsync<XmasPresentsApproved> HandlerAsync { get; } = new XmasLetterSaga(serviceBus, sagaRepository, loggerFactory);
}