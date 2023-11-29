using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;
using XmasReceiver.Facade.Adapters;
using XmasReceiver.Facade.Validators;
using XmasReceiver.Infrastructures;
using XmasReceiver.Messages.IntegrationEvents;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Entities;
using XmasReceiver.ReadModel.Queries;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Facade;

public static class ReceiverHelper
{
	public static IServiceCollection AddReceiver(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<XmasLetterContractValidator>();
		services.AddSingleton<ValidationHandler>();

		services.AddScoped<IXmasLetterService, XmasLetterService>();
		services.AddScoped<IQueries<XmasLetter>, XmasLetterQueries>();
		services.AddScoped<IReceiverFacade, ReceiverFacade>();

		services.AddScoped<IIntegrationEventHandlerAsync<XmasLetterProcessed>, XmasLetterProcessedEventHandler>();

		return services;
	}

	public static IServiceCollection AddReceiverInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);

		return services;
	}
}