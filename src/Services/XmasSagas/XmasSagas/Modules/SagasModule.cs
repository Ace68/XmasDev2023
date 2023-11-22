using XmasSagas.Facade;
using XmasSagas.Facade.Endpoints;

namespace XmasSagas.Modules;

public class SagasModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder) => builder.Services.AddSagas();

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapSagasEndpoints();
}