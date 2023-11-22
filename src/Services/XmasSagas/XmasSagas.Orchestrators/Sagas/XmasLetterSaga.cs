using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using XmasSagas.Messages.Commands;
using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Orchestrators.Sagas;

public class XmasLetterSaga : Saga<XmasLetterSaga.XmasLetterSagaState>,
	ISagaStartedByAsync<StartXmasLetterSaga>
{
	public class XmasLetterSagaState
	{
		public string SagaId { get; set; } = string.Empty;
		public XmasLetterContract Body { get; set; } = new();
	}

	public XmasLetterSaga(IServiceBus serviceBus, ISagaRepository repository, ILoggerFactory loggerFactory) : base(serviceBus, repository, loggerFactory)
	{
	}

	public Task StartedByAsync(StartXmasLetterSaga command)
	{
		throw new NotImplementedException();
	}
}