using Muflone.Messages.Events;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Messages.Events;

public sealed class WarehouseCreated(WarehouseId aggregateId) : DomainEvent(aggregateId)
{
	public readonly WarehouseId WarehouseId = aggregateId;
}