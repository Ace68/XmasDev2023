using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Transport.Azure.Models;
using XmasReceiver.Infrastructures.Azure;
using XmasReceiver.Infrastructures.MongoDb;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		string eventStoreConnectionString)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMufloneEventStore(eventStoreConnectionString);
		services.AddAzureReceiverConsumer(azureServiceBusConfiguration);

		return services;
	}

}