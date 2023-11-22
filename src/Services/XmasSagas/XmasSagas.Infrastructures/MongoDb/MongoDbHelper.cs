using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Muflone.Saga.Persistence.MongoDb;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddMongoDb(this IServiceCollection services,
		MongoDbSettings mongoDbSettings)
	{
		services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));
		services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName));

		return services;
	}
}
