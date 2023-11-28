using Muflone.Persistence;
using System.Text.Json;
using XmasReceiver.Messages.Commands;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.BindingContracts;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;

namespace XmasReceiver.Facade;

public sealed class ReceiverFacade(IXmasLetterService xmasLetterService, IServiceBus serviceBus) : IReceiverFacade
{
	public async Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken)
	{
		return await xmasLetterService.GetXmasLetterAsync(cancellationToken);
	}

	public async Task<string> PostXmasLetterAsync(XmasLetterContract xmasLetterContract, CancellationToken cancellationToken)
	{
		var receiveXmasLetter = new ReceiveXmasLetter(new XmasLetterId(Guid.NewGuid()), Guid.NewGuid(),
			new XmasLetterNumber(xmasLetterContract.XmasLetterNumber), new ReceivedOn(xmasLetterContract.ReceivedOn),
			new ChildEmail(xmasLetterContract.ChildEmail), new LetterSubject(xmasLetterContract.LetterSubject),
			new LetterBody(xmasLetterContract.LetterBody));

		var xmasSagaState = new XmasSagaState(JsonSerializer.Serialize(xmasLetterContract), false, false, false, false);
		receiveXmasLetter.UserProperties.Add("SagaState", JsonSerializer.Serialize(xmasSagaState));

		await serviceBus.SendAsync(receiveXmasLetter, cancellationToken);

		return receiveXmasLetter.AggregateId.Value.ToString();
	}
}