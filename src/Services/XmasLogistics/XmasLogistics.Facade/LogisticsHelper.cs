using Microsoft.Extensions.DependencyInjection;
using XmasLogistics.Infrastructures;
using XmasLogistics.Shared.Configurations;

namespace XmasLogistics.Facade;

public static class LogisticsHelper
{
	public static IServiceCollection AddLogistics(this IServiceCollection services)
	{
		services.AddScoped<ILogisticsFacade, LogisticsFacade>();

		return services;
	}

	public static IServiceCollection AddLogisticsInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);
		return services;
	}
}