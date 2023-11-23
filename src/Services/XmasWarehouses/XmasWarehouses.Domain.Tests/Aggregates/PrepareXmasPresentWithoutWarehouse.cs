using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using XmasWarehouses.Domain.CommandHandlers;
using XmasWarehouses.Messages.Commands;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Domain.Tests.Aggregates;

public class PrepareXmasPresentWithoutWarehouse : CommandSpecification<PrepareXmasPresents>
{
	private readonly XmasLetterId _xmasLetterId;
	private readonly WarehouseId _warehouseId;
	private readonly Guid _correlationId = Guid.NewGuid();
	private readonly LetterBody _letterBody = new("I wish a new bikecycle");

	public PrepareXmasPresentWithoutWarehouse()
	{
		var domainId = Guid.NewGuid();
		_xmasLetterId = new XmasLetterId(domainId);
		_warehouseId = new WarehouseId(domainId);
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override PrepareXmasPresents When()
	{
		return new PrepareXmasPresents(_xmasLetterId, _correlationId, _letterBody);
	}

	protected override ICommandHandlerAsync<PrepareXmasPresents> OnHandler()
	{
		return new PrepareXmasPresentsCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new WarehouseCreated(_warehouseId);
		yield return new XmasPresentsPrepared(_xmasLetterId, _correlationId, _letterBody);
	}
}