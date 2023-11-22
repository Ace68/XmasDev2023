using Microsoft.Extensions.DependencyInjection;

namespace XmasSagas.Facade;

public static class SagasHelper
{
	public static IServiceCollection AddSagas(this IServiceCollection services)
	{
		services.AddSingleton<ISagasFacade, SagasFacade>();

		return services;
	}

	public static IServiceCollection AddSagasInfrastructure(this IServiceCollection services)
	{

		return services;
	}
}