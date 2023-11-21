using XmasReceiver.Facade;
using XmasReceiver.Facade.Endpoints;

namespace XmasReceiver.Modules;

public class ReceiverModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		=> builder.Services.AddReceiver();

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) 
		=> endpoints.MapReceiverEndpoints();
}