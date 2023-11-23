using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.ReadModel.Services;

public interface IWarehousesService
{
	Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName, CancellationToken cancellationToken);
}