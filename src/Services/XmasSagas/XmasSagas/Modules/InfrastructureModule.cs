using Muflone.Transport.Azure.Models;
using XmasSagas.Infrastructures;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var azureSettings = builder.Configuration.GetSection("XmasDev:ServiceBusSettings")
			.Get<AzureServiceBusConfiguration>()!;
		var mongoDbSettings = builder.Configuration.GetSection("XmasDev:MongoDbSettings")
			.Get<MongoDbSettings>()!;

		builder.Services.AddInfrastructures(mongoDbSettings, azureSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}