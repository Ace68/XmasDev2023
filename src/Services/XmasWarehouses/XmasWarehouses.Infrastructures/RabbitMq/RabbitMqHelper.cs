using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;
using XmasWarehouses.Infrastructures.RabbitMq.Commands;
using XmasWarehouses.Infrastructures.RabbitMq.Events;
using XmasWarehouses.Shared.Configurations;

namespace XmasWarehouses.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMqForWarehousesModule(this IServiceCollection services,
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
			new PrepareXmasPresentsConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new XmasPresentsPreparedConsumer(serviceProvider.GetRequiredService<IEventBus>(), mufloneConnectionFactory, loggerFactory),
			new XmasPresentsApprovedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory)
		});
		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}