using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using XmasReceiver.Infrastructures.Azure.Commands;
using XmasReceiver.Infrastructures.Azure.Events;
using XmasReceiver.ReadModel.Services;

namespace XmasReceiver.Infrastructures.Azure;

public static class AzureHelper
{
	public static IServiceCollection AddAzureReceiverConsumer(this IServiceCollection services, AzureServiceBusConfiguration azureServiceBusConfiguration)
	{
		services.AddMufloneTransportAzure(azureServiceBusConfiguration);

		var serviceProvider = services.BuildServiceProvider();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new ReceiveXmasLetterConsumer(repository, azureServiceBusConfiguration, loggerFactory),
			new XmasLetterReceivedConsumer(serviceProvider.GetRequiredService<IXmasLetterService>(),
				serviceProvider.GetRequiredService<IEventBus>(),
				azureServiceBusConfiguration, loggerFactory)
		});
		services.AddMufloneAzureConsumers(consumers);

		return services;
	}
}