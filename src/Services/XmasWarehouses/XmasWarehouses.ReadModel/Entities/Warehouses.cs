using XmasWarehouses.ReadModel.Abstracts;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.ReadModel.Entities;

public class Warehouses : EntityBase
{
	public string Name { get; set; } = string.Empty;

	protected Warehouses()
	{ }

	internal static Warehouses CreateWarehouse(WarehouseId warehouseId, WarehouseName warehouseName) =>
		new(warehouseId.Value.ToString(), warehouseName.Value);

	private Warehouses(string warehouseId, string warehouseName)
	{
		Id = warehouseId;
		Name = warehouseName;
	}
}