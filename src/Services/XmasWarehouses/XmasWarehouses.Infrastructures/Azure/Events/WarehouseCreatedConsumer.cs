using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.EventHandlers;
using XmasWarehouses.ReadModel.Services;

namespace XmasWarehouses.Infrastructures.Azure.Events;

public sealed class WarehouseCreatedConsumer(IWarehousesService warehousesService,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null)
	: DomainEventConsumerBase<WarehouseCreated>(azureServiceBusConfiguration, loggerFactory, messageSerializer)
{
	protected override IEnumerable<IDomainEventHandlerAsync<WarehouseCreated>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<WarehouseCreated>>
	{
		new WarehouseCreatedEventHandler(loggerFactory, warehousesService)
	};
}