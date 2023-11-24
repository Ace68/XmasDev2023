using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using XmasReceiver.Infrastructures.MongoDb;
using XmasReceiver.Infrastructures.RabbitMq;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMufloneEventStore(eventStoreConnectionString);
		services.AddRabbitMqForSagasModule(rabbitMqSettings);

		return services;
	}

}