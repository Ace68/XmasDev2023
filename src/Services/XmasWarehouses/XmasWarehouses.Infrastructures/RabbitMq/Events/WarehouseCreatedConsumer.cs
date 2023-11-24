using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.EventHandlers;
using XmasWarehouses.ReadModel.Services;

namespace XmasWarehouses.Infrastructures.RabbitMq.Events;

public sealed class WarehouseCreatedConsumer(IWarehousesService warehousesService, IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<WarehouseCreated>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<WarehouseCreated>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<WarehouseCreated>>
	{
		new WarehouseCreatedEventHandler(loggerFactory, warehousesService)
	};
}