using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using XmasLogistics.Infrastructures.MongoDb;
using XmasLogistics.Infrastructures.RabbitMq;
using XmasLogistics.Shared.Configurations;

namespace XmasLogistics.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMufloneEventStore(eventStoreConnectionString);
		services.AddRabbitMqForLogisticsModule(rabbitMqSettings);

		return services;
	}

}