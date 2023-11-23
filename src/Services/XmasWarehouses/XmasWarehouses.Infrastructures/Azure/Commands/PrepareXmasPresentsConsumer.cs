using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Domain.CommandHandlers;
using XmasWarehouses.Messages.Commands;

namespace XmasWarehouses.Infrastructures.Azure.Commands;

public sealed class PrepareXmasPresentsConsumer(IRepository repository,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null)
	: CommandConsumerBase<PrepareXmasPresents>(azureServiceBusConfiguration, loggerFactory, messageSerializer)
{
	protected override ICommandHandlerAsync<PrepareXmasPresents> HandlerAsync { get; } = new PrepareXmasPresentsCommandHandler(repository, loggerFactory);
}