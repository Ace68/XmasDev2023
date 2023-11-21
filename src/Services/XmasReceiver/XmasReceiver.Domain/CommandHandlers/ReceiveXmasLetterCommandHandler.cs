using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using XmasReceiver.Domain.Aggregates;
using XmasReceiver.Messages.Commands;

namespace XmasReceiver.Domain.CommandHandlers;

public sealed class ReceiveXmasLetterCommandHandler : CommandHandlerBaseAsync<ReceiveXmasLetter>
{
	public ReceiveXmasLetterCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(ReceiveXmasLetter command, CancellationToken cancellationToken = default)
	{
		var aggregate = XmasLetter.ReceiveXmasLetter(command.XmasLetterId, command.MessageId, command.XmasLetterNumber,
			command.ReceivedOn, command.ChildEmail, command.LetterSubject, command.LetterBody);
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}