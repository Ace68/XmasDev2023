using XmasSagas.Facade;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Modules;

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

		builder.Services.AddSagasInfrastructure(mongoDbSettings, rabbitMqSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}