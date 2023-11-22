using Microsoft.Extensions.DependencyInjection;
using Muflone.Transport.Azure.Models;
using XmasSagas.Infrastructures.Azure;
using XmasSagas.Infrastructures.MongoDb;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructures(this IServiceCollection services,
		MongoDbSettings mongoDbSettings, AzureServiceBusConfiguration azureServiceBusConfiguration)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddAzureForSagasModule(azureServiceBusConfiguration);

		return services;
	}
}