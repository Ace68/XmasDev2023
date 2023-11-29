using Muflone.Core;
using System.Text.Json;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.Shared.BindingContracts;
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
		Id = warehouseId;
	}
	#endregion

	#region PrepareXmasPresents

	internal void PrepareXmasPresents(XmasLetterId xmasLetterId, Guid correlationId, LetterBody letterBody, string sagaState)
	{
		var xmasPresentsPrepared = new XmasPresentsPrepared(xmasLetterId, correlationId, letterBody);
		var newState = JsonSerializer.Deserialize<XmasSagaState>(sagaState);
		newState = newState with { XmasLetterApproved = true };
		xmasPresentsPrepared.UserProperties.Add("SagaState", JsonSerializer.Serialize(newState));

		RaiseEvent(xmasPresentsPrepared);
	}

	private void Apply(XmasPresentsPrepared @event)
	{
	}
	#endregion
}