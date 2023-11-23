using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.Services;
using XmasWarehouses.Shared.CustomTypes;

namespace XmasWarehouses.ReadModel.EventHandlers;

public sealed class WarehouseCreatedEventHandler(ILoggerFactory loggerFactory, IWarehousesService warehousesService)
	: DomainEventHandlerAsync<WarehouseCreated>(loggerFactory)
{
	public override async Task HandleAsync(WarehouseCreated @event, CancellationToken cancellationToken = new())
	{
		await warehousesService.CreateWarehouseAsync(@event.WarehouseId, new WarehouseName("New Warehouse"), cancellationToken);
	}
}