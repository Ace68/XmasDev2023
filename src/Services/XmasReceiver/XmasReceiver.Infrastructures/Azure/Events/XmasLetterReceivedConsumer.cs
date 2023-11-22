using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.ReadModel.EventHandlers;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.Infrastructures.Azure.Events;

public sealed class XmasLetterReceivedConsumer(IXmasLetterService xmasLetterService, IEventBus eventBus,
	AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
	ISerializer? messageSerializer = null) : DomainEventConsumerBase<XmasLetterReceived>(azureServiceBusConfiguration,
	loggerFactory, messageSerializer)
{
	protected override IEnumerable<IDomainEventHandlerAsync<XmasLetterReceived>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<XmasLetterReceived>>
	{
		new XmasLetterReceivedEventHandlerAsync(xmasLetterService, loggerFactory),
		new XmasLetterReceivedForIntegrationEventHandlerAsync(loggerFactory, eventBus)
	};
}