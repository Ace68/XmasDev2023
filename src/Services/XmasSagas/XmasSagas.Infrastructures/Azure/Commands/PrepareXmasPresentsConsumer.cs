using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.Azure.Commands;

public sealed class PrepareXmasPresentsConsumer : CommandSenderBase<PrepareXmasPresents>
{
	public PrepareXmasPresentsConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration) : base(azureServiceBusConfiguration)
	{
	}
}