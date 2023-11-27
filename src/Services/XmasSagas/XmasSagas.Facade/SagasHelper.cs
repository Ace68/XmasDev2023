using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using XmasSagas.Facade.Validators;
using XmasSagas.Infrastructures;
using XmasSagas.Orchestrators.Hubs;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Facade;

public static class SagasHelper
{
	public static IServiceCollection AddSagas(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<XmasLetterContractValidator>();
		services.AddSingleton<ValidationHandler>();
		services.AddSingleton<ISagasFacade, SagasFacade>();
		services.AddSingleton<XmasHub>();

		return services;
	}

	public static IServiceCollection AddSagasInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings, RabbitMqSettings rabbitMqSettings)
	{
		services.AddInfrastructures(mongoDbSettings, rabbitMqSettings);

		return services;
	}
}