using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.Azure.Commands;

public class ReceiveXmasLetterConsumer : CommandConsumerBase<ReceiveXmasLetter>
{
	protected override ICommandHandlerAsync<ReceiveXmasLetter> HandlerAsync { get; }

	public ReceiveXmasLetterConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration,
		loggerFactory, messageSerializer)
	{

	}

	public async Task ReceiveXmasLetterSenderHandlerAsync(ReceiveXmasLetter command)
	{

	}
}