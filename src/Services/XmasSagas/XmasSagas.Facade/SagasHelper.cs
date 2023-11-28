using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Saga;
using XmasSagas.Facade.Validators;
using XmasSagas.Infrastructures;
using XmasSagas.Messages.Commands;
using XmasSagas.Messages.IntegrationEvents;
using XmasSagas.Orchestrators.Hubs;
using XmasSagas.Orchestrators.Sagas;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Facade;

public static class SagasHelper
{
	public static IServiceCollection AddSagas(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<XmasLetterContractValidator>();
		services.AddSingleton<ValidationHandler>();
		services.AddScoped<ISagasFacade, SagasFacade>();
		services.AddScoped<IHubsHelper, HubsHelper>();

		services.AddScoped<ISagaStartedByAsync<StartXmasLetterSaga>, XmasLetterSaga>();
		services.AddScoped<ISagaEventHandlerAsync<XmasPresentsApproved>, XmasLetterSaga>();
		services.AddScoped<ISagaEventHandlerAsync<XmasPresentsReadyToSend>, XmasLetterSaga>();
		services.AddScoped<ISagaEventHandlerAsync<XmasLetterProcessed>, XmasLetterSaga>();
		services.AddScoped<ISagaEventHandlerAsync<XmasSagaCompleted>, XmasLetterSaga>();

		return services;
	}

	public static IServiceCollection AddSagasInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings, RabbitMqSettings rabbitMqSettings)
	{
		services.AddInfrastructures(mongoDbSettings, rabbitMqSettings);

		return services;
	}
}