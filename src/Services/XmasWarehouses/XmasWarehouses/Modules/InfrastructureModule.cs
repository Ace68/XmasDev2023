using XmasWarehouses.Facade;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var rabbitMqSettings = builder.Configuration.GetSection("XmasDev:RabbitMqSettings")
			.Get<RabbitMqSettings>()!;
		var mongoDbSettings = builder.Configuration.GetSection("XmasDev:MongoDbSettings")
			.Get<MongoDbSettings>()!;

		builder.Services.AddWarehousesInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["XmasDev:EventStore:ConnectionString"]!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}