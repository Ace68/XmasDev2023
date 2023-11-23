namespace XmasWarehouses.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		//var azureServiceBusSettings = builder.Configuration.GetSection("XmasDev:ServiceBusSettings")
		//	.Get<AzureServiceBusConfiguration>()!;
		//var mongoDbSettings = builder.Configuration.GetSection("XmasDev:MongoDbSettings")
		//	.Get<MongoDbSettings>()!;

		//builder.Services.AddSagasInfrastructure(mongoDbSettings, azureServiceBusSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}