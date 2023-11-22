using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using XmasSagas.Infrastructures.Azure.Commands;

namespace XmasSagas.Infrastructures.Azure;

public static class AzureHelper
{
	public static IServiceCollection AddAzureForSagasModule(this IServiceCollection services,
		AzureServiceBusConfiguration azureServiceBusConfiguration)
	{
		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		services.AddMufloneTransportAzure(azureServiceBusConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var serviceBus = serviceProvider.GetRequiredService<IServiceBus>();
		var sagaRepository = serviceProvider.GetRequiredService<ISagaRepository>();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new StartXmasLetterSagaConsumer(serviceBus, sagaRepository, azureServiceBusConfiguration, loggerFactory),
			new ReceiveXmasLetterConsumer(azureServiceBusConfiguration),
		});
		services.AddMufloneAzureConsumers(consumers);

		return services;
	}
}