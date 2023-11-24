using XmasReceiver.Facade;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var mongoDbSettings = builder.Configuration.GetSection("XmasDev:MongoDbSettings")
			.Get<MongoDbSettings>()!;
		var rabbitMqSettings = builder.Configuration.GetSection("XmasDev:RabbitMqSettings")
			.Get<RabbitMqSettings>()!;

		builder.Services.AddReceiverInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["XmasDev:EventStore:ConnectionString"]!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}