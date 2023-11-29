using Muflone.Core;
using System.Text.Json;
using XmasLogistics.Messages.Events;
using XmasLogistics.Shared.BindingContracts;
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
	internal void SentXmasPresents(XmasLetterId xmasLetterId, Guid correlationId, LetterBody letterBody, string sagaState)
	{
		var xmasPresentsSent = new XmasPresentsSent(xmasLetterId, correlationId, letterBody);
		var newState = JsonSerializer.Deserialize<XmasSagaState>(sagaState);
		newState = newState with { XmasLetterProcessed = true };
		xmasPresentsSent.UserProperties.Add("SagaState", JsonSerializer.Serialize(newState));

		RaiseEvent(xmasPresentsSent);
	}

	private void Apply(XmasPresentsSent @event)
	{
	}
	#endregion
}