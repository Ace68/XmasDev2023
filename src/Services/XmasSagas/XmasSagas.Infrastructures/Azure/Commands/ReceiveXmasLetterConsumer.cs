using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.Azure.Commands;

public class ReceiveXmasLetterConsumer : CommandSenderBase<ReceiveXmasLetter>
{
	public ReceiveXmasLetterConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration) : base(azureServiceBusConfiguration)
	{
	}
}