using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;
using XmasReceiver.Infrastructures.RabbitMq.Commands;
using XmasReceiver.Infrastructures.RabbitMq.Events;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.Configurations;

namespace XmasReceiver.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMqForSagasModule(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
			rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new ReceiveXmasLetterConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new XmasLetterReceivedConsumer(serviceProvider.GetRequiredService<IXmasLetterService>(),
				serviceProvider.GetRequiredService<IEventBus>(), mufloneConnectionFactory, loggerFactory),
			new CloseXmasLetterConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new XmasLetterClosedConsumer(serviceProvider.GetRequiredService<IXmasLetterService>(),
				serviceProvider.GetRequiredService<IEventBus>(), mufloneConnectionFactory, loggerFactory),

			new XmasLetterProcessedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory)
		});
		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}