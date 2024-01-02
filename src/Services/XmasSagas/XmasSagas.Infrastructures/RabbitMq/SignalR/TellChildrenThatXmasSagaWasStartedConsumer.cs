using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.RabbitMq.SignalR;

public class TellChildrenThatXmasSagaWasStartedConsumer : CommandSenderBase<TellChildrenThatXmasSagaWasStarted>
{
	public TellChildrenThatXmasSagaWasStartedConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
		: base(connectionFactory, loggerFactory)
	{
	}
}