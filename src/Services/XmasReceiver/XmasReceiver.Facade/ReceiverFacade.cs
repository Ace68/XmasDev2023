using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.BindingContracts;

namespace XmasReceiver.Facade;

public sealed class ReceiverFacade(IXmasLetterService xmasLetterService) : IReceiverFacade
{
	public async Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken)
	{
		return await xmasLetterService.GetXmasLetterAsync(cancellationToken);
	}
}