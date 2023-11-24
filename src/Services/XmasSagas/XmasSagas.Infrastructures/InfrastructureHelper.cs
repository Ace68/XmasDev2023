using Microsoft.Extensions.DependencyInjection;
using XmasSagas.Infrastructures.MongoDb;
using XmasSagas.Infrastructures.RabbitMq;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructures(this IServiceCollection services,
		MongoDbSettings mongoDbSettings, RabbitMqSettings rabbitMqSettings)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddRabbitMqForSagasModule(rabbitMqSettings);

		return services;
	}
}