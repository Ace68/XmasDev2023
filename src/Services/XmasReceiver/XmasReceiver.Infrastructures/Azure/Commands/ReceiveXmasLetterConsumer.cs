using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasReceiver.Domain.CommandHandlers;
using XmasReceiver.Messages.Commands;

namespace XmasReceiver.Infrastructures.Azure.Commands;

public sealed class ReceiveXmasLetterConsumer : CommandConsumerBase<ReceiveXmasLetter>
{
	protected override ICommandHandlerAsync<ReceiveXmasLetter> HandlerAsync { get; }

	public ReceiveXmasLetterConsumer(IRepository repository, AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration,
		loggerFactory, messageSerializer)
	{
		HandlerAsync = new ReceiveXmasLetterCommandHandlerAsync(repository, loggerFactory);
	}
}