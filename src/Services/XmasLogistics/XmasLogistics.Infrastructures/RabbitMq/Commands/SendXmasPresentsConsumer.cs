using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasLogistics.Domain.CommandHandlers;
using XmasLogistics.Messages.Commands;

namespace XmasLogistics.Infrastructures.RabbitMq.Commands;

public sealed class SendXmasPresentsConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<SendXmasPresents>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<SendXmasPresents> HandlerAsync { get; } = new SendXmasPresentsCommandHandler(repository, loggerFactory);
}