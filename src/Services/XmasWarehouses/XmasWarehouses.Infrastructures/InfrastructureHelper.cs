using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Infrastructures.Azure;
using XmasWarehouses.Infrastructures.MongoDb;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Infrastructures;

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