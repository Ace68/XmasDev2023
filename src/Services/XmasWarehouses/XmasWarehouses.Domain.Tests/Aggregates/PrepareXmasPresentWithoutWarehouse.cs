using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using System.Text.Json;
using XmasWarehouses.Domain.CommandHandlers;
using XmasWarehouses.Messages.Commands;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.Shared.BindingContracts;
using XmasWarehouses.Shared.CustomTypes;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Domain.Tests.Aggregates;

public class PrepareXmasPresentWithoutWarehouse : CommandSpecification<PrepareXmasPresents>
{
	private readonly XmasLetterId _xmasLetterId;
	private readonly WarehouseId _warehouseId;
	private readonly Guid _correlationId = Guid.NewGuid();
	private readonly LetterBody _letterBody = new("I wish a new bikecycle");

	private XmasSagaState _xmasSagaState;
	private readonly XmasLetterContract _xmasLetterContract = new();

	public PrepareXmasPresentWithoutWarehouse()
	{
		var domainId = Guid.NewGuid();
		_xmasLetterId = new XmasLetterId(domainId);
		_warehouseId = new WarehouseId(domainId);

		_xmasLetterContract.XmasLetterNumber = $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}";
		_xmasLetterContract.ChildEmail = "child@xmas.com";
		_xmasLetterContract.LetterSubject = "XmasLetter";
		_xmasLetterContract.ReceivedOn = DateTime.UtcNow;
		_xmasLetterContract.LetterBody = _letterBody.Value;

		_xmasSagaState = new XmasSagaState(JsonSerializer.Serialize(_xmasLetterContract), true, false, false, false);
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override PrepareXmasPresents When()
	{
		var prepareXmasPresents = new PrepareXmasPresents(_xmasLetterId, _correlationId, _letterBody);
		prepareXmasPresents.UserProperties.Add("SagaState", JsonSerializer.Serialize(_xmasSagaState));

		return prepareXmasPresents;
	}

	protected override ICommandHandlerAsync<PrepareXmasPresents> OnHandler()
	{
		return new PrepareXmasPresentsCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		var xmasPresentsPrepared = new XmasPresentsPrepared(_xmasLetterId, _correlationId, _letterBody);
		_xmasSagaState = new XmasSagaState(JsonSerializer.Serialize(_xmasLetterContract), true, true, false, false);
		xmasPresentsPrepared.UserProperties.Add("SagaState", JsonSerializer.Serialize(_xmasSagaState));

		yield return xmasPresentsPrepared;
	}
}