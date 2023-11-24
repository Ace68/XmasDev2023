using Muflone.Core;
using XmasLogistics.Messages.Events;
using XmasLogistics.Shared.CustomTypes;
using XmasLogistics.Shared.DomainIds;

namespace XmasLogistics.Domain.Aggregates;

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
		Id = warehouseId;
	}
	#endregion

	#region SentXmasPresents

	internal void SentXmasPresents(XmasLetterId xmasLetterId, Guid correlationId, LetterBody letterBody)
	{
		RaiseEvent(new XmasPresentsSent(xmasLetterId, correlationId, letterBody));
	}

	private void Apply(XmasPresentsSent @event)
	{
	}
	#endregion
}