using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasWarehouses.Domain.CommandHandlers;
using XmasWarehouses.Messages.Commands;

namespace XmasWarehouses.Infrastructures.RabbitMq.Commands;

public sealed class PrepareXmasPresentsConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<PrepareXmasPresents>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<PrepareXmasPresents> HandlerAsync { get; } = new PrepareXmasPresentsCommandHandler(repository, loggerFactory);
}