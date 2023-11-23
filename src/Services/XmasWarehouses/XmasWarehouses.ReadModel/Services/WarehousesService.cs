using Microsoft.Extensions.Logging;
using XmasWarehouses.ReadModel.Abstracts;
using XmasWarehouses.ReadModel.Entities;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.ReadModel.Services;

public class WarehousesService : BaseService, IWarehousesService
{
	public WarehousesService(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{
	}

	public async Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName, CancellationToken cancellationToken)
	{
		var entity = Warehouses.CreateWarehouse(warehouseId, warehouseName);
		await Persister.InsertAsync(entity, cancellationToken);
	}
}