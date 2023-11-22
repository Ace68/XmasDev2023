using Muflone.Persistence;
using XmasSagas.Facade.Converters;
using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Facade;

public sealed class SagasFacade : ISagasFacade
{
	private readonly IServiceBus _serviceBus;

	public SagasFacade(IServiceBus serviceBus)
	{
		_serviceBus = serviceBus;
	}

	public async Task SendXmasLettersAsync(XmasLetterContract body, CancellationToken cancellationToken)
	{
		await _serviceBus.SendAsync(body.ToCommand(), cancellationToken);
	}
}