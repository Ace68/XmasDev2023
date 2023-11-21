using XmasReceiver.Infrastructures.MongoDb;
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

		builder.Services.AddMongoDb(mongoDbSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}