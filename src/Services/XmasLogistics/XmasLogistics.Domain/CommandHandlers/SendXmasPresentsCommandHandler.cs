using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using XmasLogistics.Domain.Aggregates;
using XmasLogistics.Messages.Commands;
using XmasLogistics.Shared.DomainIds;

namespace XmasLogistics.Domain.CommandHandlers;

public sealed class SendXmasPresentsCommandHandler : CommandHandlerBaseAsync<SendXmasPresents>
{
	public SendXmasPresentsCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(SendXmasPresents command, CancellationToken cancellationToken = default)
	{
		command.UserProperties.TryGetValue("SagaState", out var sagaState);

		var aggregate = Warehouse.CreateWarehouse(new WarehouseId(command.XmasLetterId.Value));
		aggregate.SentXmasPresents(command.XmasLetterId, command.MessageId, command.LetterBody, sagaState.ToString());
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}