using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Facade;

public sealed class SagasFacade : ISagasFacade
{
	public Task SendXmasLettersAsync(XmasLetterContract body, CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}