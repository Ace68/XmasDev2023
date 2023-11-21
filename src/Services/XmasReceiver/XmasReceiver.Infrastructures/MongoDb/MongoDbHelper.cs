using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;
using XmasReceiver.Infrastructures.MongoDb.Readmodel;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddMongoDb(this IServiceCollection services,
		MongoDbSettings mongoDbSettings)
	{
		services.AddSingleton<IMongoDatabase>(x =>
		{
			BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

			var client = new MongoClient(mongoDbSettings.ConnectionString);
			var database = client.GetDatabase(mongoDbSettings.DatabaseName);
			return database;
		});

		services.AddScoped<IPersister, Persister>();

		services.AddSingleton<IEventStorePositionRepository>(x =>
			new EventStorePositionRepository(x.GetRequiredService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

		return services;
	}
}