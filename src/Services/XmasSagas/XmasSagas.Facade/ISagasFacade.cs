using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Facade;

public interface ISagasFacade
{
	Task SendXmasLettersAsync(XmasLetterContract body, CancellationToken cancellationToken);
}