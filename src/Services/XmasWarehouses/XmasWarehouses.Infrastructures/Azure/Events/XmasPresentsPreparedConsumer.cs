using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.EventHandlers;

namespace XmasWarehouses.Infrastructures.Azure.Events;

public sealed class XmasPresentsPreparedConsumer(IEventBus eventBus,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null)
	: DomainEventConsumerBase<XmasPresentsPrepared>(azureServiceBusConfiguration,
	loggerFactory, messageSerializer)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasPresentsPrepared>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasPresentsPrepared>>
	{
		new XmasPresentsPreparedForIntegrationEventHandler(loggerFactory, eventBus)
	};
}