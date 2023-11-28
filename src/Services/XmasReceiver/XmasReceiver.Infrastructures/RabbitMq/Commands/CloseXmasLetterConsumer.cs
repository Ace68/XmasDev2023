using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasReceiver.Domain.CommandHandlers;
using XmasReceiver.Messages.Commands;

namespace XmasReceiver.Infrastructures.RabbitMq.Commands;

public sealed class CloseXmasLetterConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CloseXmasLetter>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<CloseXmasLetter> HandlerAsync { get; } = new CloseXmasLetterCommandHandlerAsync(repository, loggerFactory);
}