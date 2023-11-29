using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;
using XmasWarehouses.Facade.Adapters;
using XmasWarehouses.Infrastructures;
using XmasWarehouses.Messages.Events;
using XmasWarehouses.ReadModel.Services;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Facade;

public static class WarehousesHelper
{
	public static IServiceCollection AddWarehouses(this IServiceCollection services)
	{
		services.AddScoped<IWarehousesService, WarehousesService>();
		services.AddScoped<IWarehousesFacade, WarehousesFacade>();

		services.AddScoped<IIntegrationEventHandlerAsync<XmasPresentsApproved>, XmasPresentsApprovedEventHandler>();

		return services;
	}

	public static IServiceCollection AddWarehousesInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);
		return services;
	}
}