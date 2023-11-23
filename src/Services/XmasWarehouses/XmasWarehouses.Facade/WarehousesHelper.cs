using Microsoft.Extensions.DependencyInjection;
using Muflone.Transport.Azure.Models;
using XmasWarehouses.Infrastructures;
using XmasWarehouses.ReadModel.Services;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Facade;

public static class WarehousesHelper
{
	public static IServiceCollection AddWarehouses(this IServiceCollection services)
	{
		services.AddScoped<IWarehousesService, WarehousesService>();
		services.AddScoped<IWarehousesFacade, WarehousesFacade>();

		return services;
	}

	public static IServiceCollection AddWarehousesInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		string eventStoreConnectionString)
	{
		services.AddInfrastructure(mongoDbSettings, azureServiceBusConfiguration, eventStoreConnectionString);
		return services;
	}
}