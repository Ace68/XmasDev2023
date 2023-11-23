using Muflone.Core;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Domain.Aggregates;

public class Warehouse : AggregateRoot
{
	private WarehouseId _warehouseId;

	protected Warehouse()
	{
	}

	#region Create
	internal static Warehouse CreateWarehouse(WarehouseId warehouseId)
	{
		return new(warehouseId);
	}

	private Warehouse(WarehouseId warehouseId)
	{
		RaiseEvent(new WarehouseCreated(warehouseId));
	}

	private void Apply(WarehouseCreated @event)
	{
		_warehouseId = @event.WarehouseId;
	}
	#endregion

	#region PrepareXmasPresents

	internal void PrepareXmasPresents(XmasLetterId xmasLetterId, Guid correlationId, LetterBody letterBody)
	{
		RaiseEvent(new XmasPresentsPrepared(xmasLetterId, correlationId, letterBody));
	}

	private void Apply(XmasPresentsPrepared @event)
	{
	}
	#endregion
}