using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.RabbitMq.Commands;

public class ReceiveXmasLetterConsumer : CommandSenderBase<ReceiveXmasLetter>
{
	public ReceiveXmasLetterConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) : base(
		connectionFactory, loggerFactory)
	{
	}
}