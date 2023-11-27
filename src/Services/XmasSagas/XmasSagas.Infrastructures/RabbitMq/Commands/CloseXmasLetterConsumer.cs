﻿using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using XmasSagas.Messages.Commands;

namespace XmasSagas.Infrastructures.RabbitMq.Commands;

public sealed class CloseXmasLetterConsumer : CommandSenderBase<CloseXmasLetter>
{
	public CloseXmasLetterConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
		: base(connectionFactory, loggerFactory)
	{
	}
}