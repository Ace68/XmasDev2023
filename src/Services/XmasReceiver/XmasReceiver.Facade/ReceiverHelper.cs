using Microsoft.Extensions.DependencyInjection;
using XmasReceiver.Infrastructures.MongoDb;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Entities;
using XmasReceiver.ReadModel.Queries;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Facade;

public static class ReceiverHelper
{
	public static IServiceCollection AddReceiver(this IServiceCollection services)
	{
		services.AddScoped<IXmasLetterService, XmasLetterService>();
		services.AddScoped<IQueries<XmasLetter>, XmasLetterQueries>();

		return services;
	}

	public static IServiceCollection AddReceiverInfrastructure(this IServiceCollection services, MongoDbSettings mongoDbSettings)
	{
		services.AddMongoDb(mongoDbSettings);

		return services;
	}
}