﻿using Muflone.Core;

namespace XmasWarehouses.Shared.DomainIds;

public sealed class WarehouseId : DomainId
{
	public WarehouseId(Guid value) : base(value)
	{
	}
}