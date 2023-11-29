using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using XmasWarehouses.Domain.Aggregates;
using XmasWarehouses.Messages.Commands;
using XmasWarehouses.Shared.DomainIds;

namespace XmasWarehouses.Domain.CommandHandlers;

public sealed class PrepareXmasPresentsCommandHandler : CommandHandlerBaseAsync<PrepareXmasPresents>
{
	public PrepareXmasPresentsCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(PrepareXmasPresents command, CancellationToken cancellationToken = default)
	{
		command.UserProperties.TryGetValue("SagaState", out var sagaState);

		var aggregate = Warehouse.CreateWarehouse(new WarehouseId(command.XmasLetterId.Value));
		aggregate.PrepareXmasPresents(command.XmasLetterId, command.MessageId, command.LetterBody, sagaState.ToString());

		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}