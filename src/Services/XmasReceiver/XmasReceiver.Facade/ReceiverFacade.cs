using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Services;
using XmasReceiver.Shared.BindingContracts;

namespace XmasReceiver.Facade;

public sealed class ReceiverFacade : IReceiverFacade
{
    private readonly IXmasLetterService _xmasLetterService;

    public ReceiverFacade(IXmasLetterService xmasLetterService)
    {
        _xmasLetterService = xmasLetterService;
    }

    public async Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken)
    {
        return await _xmasLetterService.GetXmasLetterAsync(cancellationToken);
    }
}