using Muflone.Transport.Azure.Models;
using XmasReceiver.Infrastructures;
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
		var azureSettings = builder.Configuration.GetSection("XmasDev:ServiceBusSettings")
			.Get<AzureServiceBusConfiguration>()!;

		builder.Services.AddInfrastructure(mongoDbSettings, azureSettings, builder.Configuration["XmasDev:EventStore:ConnectionString"]!);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}