﻿using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using XmasReceiver.Domain.Aggregates;
using XmasReceiver.Messages.Commands;

namespace XmasReceiver.Domain.CommandHandlers;

public sealed class CloseXmasLetterCommandHandlerAsync : CommandHandlerBaseAsync<CloseXmasLetter>
{
	public CloseXmasLetterCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(CloseXmasLetter command, CancellationToken cancellationToken = default)
	{
		var aggregate = await Repository.GetByIdAsync<XmasLetter>(command.AggregateId.Value);
		aggregate.CloseXmasLetter(command.MessageId);
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}