using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using XmasWarehouses.Infrastructures.MongoDb;
using XmasWarehouses.Infrastructures.RabbitMq;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMufloneEventStore(eventStoreConnectionString);
		services.AddRabbitMqForWarehousesModule(rabbitMqSettings);

		return services;
	}

}