using Microsoft.Extensions.DependencyInjection;
using XmasReceiver.Infrastructures.MongoDb;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Facade;

public static class ReceiverHelper
{
	public static IServiceCollection AddReceiver(this IServiceCollection services)
	{
		services.AddScoped<IXmasLetterService, XmasLetterService>();

		return services;
	}

	public static IServiceCollection AddReceiverInfrastructure(this IServiceCollection services, MongoDbSettings mongoDbSettings)
	{
		services.AddMongoDb(mongoDbSettings);

		return services;
	}
}