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
		var aggregate = await Repository.GetByIdAsync<Warehouse>(command.XmasLetterId.Value);
		if (aggregate == null || aggregate.Id == null)
		{
			aggregate = Warehouse.CreateWarehouse(new WarehouseId(command.XmasLetterId.Value));
		}
		aggregate.PrepareXmasPresents(command.XmasLetterId, command.MessageId, command.LetterBody);
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}