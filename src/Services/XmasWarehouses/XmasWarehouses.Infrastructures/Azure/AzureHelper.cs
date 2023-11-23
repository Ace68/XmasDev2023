using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Infrastructures.Azure.Commands;
using XmasWarehouses.Infrastructures.Azure.Events;
using XmasWarehouses.ReadModel.Services;

namespace XmasWarehouses.Infrastructures.Azure;

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
			new PrepareXmasPresentsConsumer(repository, azureServiceBusConfiguration, loggerFactory),
			new WarehouseCreatedConsumer(serviceProvider.GetRequiredService<IWarehousesService>(), azureServiceBusConfiguration, loggerFactory),
			new XmasPresentsPreparedConsumer(serviceProvider.GetRequiredService<IEventBus>(), azureServiceBusConfiguration, loggerFactory)

		});
		services.AddMufloneAzureConsumers(consumers);

		return services;
	}
}