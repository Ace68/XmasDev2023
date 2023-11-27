using XmasSagas.Orchestrators.Hubs;

namespace XmasSagas.Modules;

public class WebSocketModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;
	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddSignalR();

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapHub<XmasHub>("/xmasHub");

		return endpoints;
	}
}