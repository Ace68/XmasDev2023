using Muflone.Transport.Azure.Models;
using XmasWarehouses.Facade;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var azureServiceBusSettings = builder.Configuration.GetSection("XmasDev:ServiceBusSettings")
			.Get<AzureServiceBusConfiguration>()!;
		var mongoDbSettings = builder.Configuration.GetSection("XmasDev:MongoDbSettings")
			.Get<MongoDbSettings>()!;

		builder.Services.AddWarehousesInfrastructure(mongoDbSettings, azureServiceBusSettings, builder.Configuration["XmasDev:EventStore:ConnectionString"]!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}