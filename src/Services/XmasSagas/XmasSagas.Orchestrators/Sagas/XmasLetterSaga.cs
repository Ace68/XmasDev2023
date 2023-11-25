using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;
using XmasSagas.Messages.Commands;
using XmasSagas.Messages.IntegrationEvents;
using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Orchestrators.Sagas;

public class XmasLetterSaga : Saga<XmasLetterSaga.XmasLetterSagaState>,
	ISagaStartedByAsync<StartXmasLetterSaga>,
	ISagaEventHandlerAsync<XmasPresentsApproved>,
	ISagaEventHandlerAsync<XmasPresentsReadyToSend>,
	ISagaEventHandlerAsync<XmasLetterProcessed>,
	ISagaEventHandlerAsync<XmasSagaCompleted>
{
	public class XmasLetterSagaState
	{
		public string SagaId { get; set; } = string.Empty;
		public XmasLetterContract Body { get; set; } = new();

		public bool XmasPresentsApproved { get; set; }
		public bool XmasPresentsReadyToSend { get; set; }
		public bool XmasLetterProcessed { get; set; }
		public bool XmasSagaClosed { get; set; }

		public DateTime StartedOn { get; set; } = DateTime.UtcNow;
		public DateTime CompletedOn { get; set; } = DateTime.MinValue;
	}

	public XmasLetterSaga(IServiceBus serviceBus, ISagaRepository repository, ILoggerFactory loggerFactory)
		: base(serviceBus, repository, loggerFactory)
	{
	}

	public async Task StartedByAsync(StartXmasLetterSaga command)
	{
		SagaState = new XmasLetterSagaState
		{
			SagaId = command.MessageId.ToString(),
			Body = new XmasLetterContract
			{
				XmasLetterNumber = command.XmasLetterNumber.Value,
				ReceivedOn = command.ReceivedOn.Value,
				ChildEmail = command.ChildEmail.Value,
				LetterSubject = command.LetterSubject.Value,
				LetterBody = command.LetterBody.Value
			}
		};
		await Repository.SaveAsync(command.MessageId, SagaState);

		var receiveXmasLetter = new ReceiveXmasLetter(command.XmasLetterId, command.MessageId, command.XmasLetterNumber,
						command.ReceivedOn, command.ChildEmail, command.LetterSubject, command.LetterBody);
		await ServiceBus.SendAsync(receiveXmasLetter, CancellationToken.None);
	}

	public async Task HandleAsync(XmasPresentsApproved @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<XmasLetterSagaState>(correlationId);
		sagaState.XmasPresentsApproved = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await ServiceBus.SendAsync(new PrepareXmasPresents(@event.XmasLetterId, correlationId, @event.LetterBody), CancellationToken.None);
	}

	public async Task HandleAsync(XmasPresentsReadyToSend @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<XmasLetterSagaState>(correlationId);
		sagaState.XmasPresentsReadyToSend = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await ServiceBus.SendAsync(new SendXmasPresents(@event.XmasLetterId, correlationId, @event.LetterBody),
			CancellationToken.None);
	}

	public async Task HandleAsync(XmasLetterProcessed @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<XmasLetterSagaState>(correlationId);
		sagaState.XmasLetterProcessed = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await ServiceBus.SendAsync(new CloseXmasLetter(@event.XmasLetterId, correlationId));
	}

	public async Task HandleAsync(XmasSagaCompleted @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<XmasLetterSagaState>(correlationId);
		sagaState.XmasSagaClosed = true;
		await Repository.SaveAsync(correlationId, sagaState);
	}
}