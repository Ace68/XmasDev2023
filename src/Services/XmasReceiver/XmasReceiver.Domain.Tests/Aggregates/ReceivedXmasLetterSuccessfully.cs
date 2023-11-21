using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using XmasReceiver.Domain.CommandHandlers;
using XmasReceiver.Messages.Commands;
using XmasReceiver.Messages.DomainEvents;
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

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override ReceiveXmasLetter When()
	{
		return new ReceiveXmasLetter(_xmasLetterId, _commitId, _xmasLetterNumber, _receivedOn, _childEmail, _letterSubject, _letterBody);
	}

	protected override ICommandHandlerAsync<ReceiveXmasLetter> OnHandler()
	{
		return new ReceiveXmasLetterCommandHandlerAsync(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new XmasLetterReceived(_xmasLetterId, _commitId, _xmasLetterNumber, _receivedOn, _childEmail, _letterSubject, _letterBody, XmasLetterStatus.Received);
	}
}