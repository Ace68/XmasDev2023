using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using System.Text.Json;
using XmasReceiver.Domain.CommandHandlers;
using XmasReceiver.Messages.Commands;
using XmasReceiver.Messages.DomainEvents;
using XmasReceiver.Shared.BindingContracts;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.Domain.Tests.Aggregates;

public sealed class ReceivedXmasLetterSuccessfully : CommandSpecification<ReceiveXmasLetter>
{
	private readonly XmasLetterId _xmasLetterId = new(Guid.NewGuid());
	private readonly XmasLetterNumber _xmasLetterNumber = new($"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}");

	private readonly Guid _commitId = Guid.NewGuid();

	private readonly ReceivedOn _receivedOn = new(DateTime.UtcNow);
	private readonly ChildEmail _childEmail = new("child@xmas.com");
	private readonly LetterSubject _letterSubject = new("Dear Santa");
	private readonly LetterBody _letterBody = new("I want a new bike");

	private readonly XmasLetterContract _xmasLetterContract = new();

	public ReceivedXmasLetterSuccessfully()
	{
		_xmasLetterContract.XmasLetterNumber = _xmasLetterNumber.Value;
		_xmasLetterContract.ChildEmail = _childEmail.Value;
		_xmasLetterContract.LetterSubject = _letterSubject.Value;
		_xmasLetterContract.ReceivedOn = _receivedOn.Value;
		_xmasLetterContract.LetterBody = _letterBody.Value;
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override ReceiveXmasLetter When()
	{
		var receiveXmasLetter = new ReceiveXmasLetter(_xmasLetterId, _commitId, _xmasLetterNumber, _receivedOn, _childEmail, _letterSubject, _letterBody);
		receiveXmasLetter.UserProperties.Add("SagaState",
			JsonSerializer.Serialize(new XmasSagaState(JsonSerializer.Serialize(_xmasLetterContract), false, false,
				false, false)));
		return receiveXmasLetter;
	}

	protected override ICommandHandlerAsync<ReceiveXmasLetter> OnHandler()
	{
		return new ReceiveXmasLetterCommandHandlerAsync(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		var xmasLetterReceived = new XmasLetterReceived(_xmasLetterId, _commitId, _xmasLetterNumber, _receivedOn,
			_childEmail, _letterSubject, _letterBody, XmasLetterStatus.Received);
		xmasLetterReceived.UserProperties.Add("SagaState", JsonSerializer.Serialize(new XmasSagaState(JsonSerializer.Serialize(_xmasLetterContract), true, false, false, false)));

		yield return xmasLetterReceived;
	}
}