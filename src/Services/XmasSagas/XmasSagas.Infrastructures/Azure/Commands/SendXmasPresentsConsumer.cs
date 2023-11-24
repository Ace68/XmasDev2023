using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.Azure.Commands;

public sealed class SendXmasPresentsConsumer : CommandSenderBase<SendXmasPresents>
{
	public SendXmasPresentsConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration) : base(azureServiceBusConfiguration)
	{
	}
}