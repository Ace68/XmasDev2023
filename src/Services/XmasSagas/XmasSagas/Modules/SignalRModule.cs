using XmasSagas.Facade.Endpoints;

namespace XmasSagas.Modules;

public class SignalRModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var signalRSettings = builder.Configuration["XmasDev:AzureSignarlRSettings:ConnectionString"];
		builder.Services.AddSignalR()
			.AddAzureSignalR(builder.Configuration["XmasDev:AzureSignarlRSettings:ConnectionString"]);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapSignalR();
}