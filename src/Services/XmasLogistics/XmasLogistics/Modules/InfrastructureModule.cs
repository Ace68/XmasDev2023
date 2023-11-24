using XmasLogistics.Facade;
using XmasLogistics.Shared.Configurations;

namespace XmasLogistics.Modules;

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

		builder.Services.AddLogisticsInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["XmasDev:EventStore:ConnectionString"]!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}