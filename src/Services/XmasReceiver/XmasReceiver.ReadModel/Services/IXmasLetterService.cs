using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.Shared.BindingContracts;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.ReadModel.Services;

public interface IXmasLetterService
{
	Task ReceiveLetterAsync(XmasLetterId aggregateId, XmasLetterNumber xmasLetterNumber, ReceivedOn receivedOn,
		ChildEmail childEmail, LetterSubject letterSubject, LetterBody letterBody, XmasLetterStatus xmasLetterStatus,
		CancellationToken cancellationToken = default);
	
	Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken);
}