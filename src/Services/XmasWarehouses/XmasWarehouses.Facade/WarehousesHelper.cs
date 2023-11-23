using Microsoft.Extensions.DependencyInjection;

namespace XmasWarehouses.Facade;

public static class WarehousesHelper
{
	public static IServiceCollection AddWarehouses(this IServiceCollection services)
	{
		services.AddSingleton<IWarehousesFacade, WarehousesFacade>();

		return services;
	}
}