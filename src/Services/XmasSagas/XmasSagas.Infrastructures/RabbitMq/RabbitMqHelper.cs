﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;
using XmasSagas.Infrastructures.RabbitMq.Commands;
using XmasSagas.Infrastructures.RabbitMq.Events;
using XmasSagas.Shared.Configurations;

namespace XmasSagas.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMqForSagasModule(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
			rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var serviceBus = serviceProvider.GetRequiredService<IServiceBus>();
		var sagaRepository = serviceProvider.GetRequiredService<ISagaRepository>();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		var consumerConfiguration = new ConsumerConfiguration
		{
			QueueName = "StartXmasLetterSaga",
			ResourceKey = "StartXmasLetterSaga"
		};
		consumers = consumers.Concat(new List<IConsumer>
		{
			new StartXmasLetterSagaConsumer(serviceBus, sagaRepository, consumerConfiguration, mufloneConnectionFactory, loggerFactory),
			new ReceiveXmasLetterConsumer(mufloneConnectionFactory, loggerFactory),
			new XmasPresentsApprovedConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
			new PrepareXmasPresentsConsumer(mufloneConnectionFactory , loggerFactory),
			new XmasPresentsReadyToSendConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
			new SendXmasPresentsConsumer(mufloneConnectionFactory, loggerFactory),
			new XmasPresentsApprovedConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
			new CloseXmasLetterConsumer(mufloneConnectionFactory, loggerFactory),
			new XmasLetterProcessedConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
			new XmasSagaCompletedConsumer(serviceBus, sagaRepository, mufloneConnectionFactory, loggerFactory),
		});
		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}