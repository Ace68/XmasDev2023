using Microsoft.Extensions.Logging;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Entities;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.ReadModel.Services;

public sealed class XmasLetterService : BaseService, IXmasLetterService
{
	public XmasLetterService(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{
	}

	public async Task ReceiveLetterAsync(XmasLetterId aggregateId, XmasLetterNumber xmasLetterNumber, ReceivedOn receivedOn,
		ChildEmail childEmail, LetterSubject letterSubject, LetterBody letterBody, XmasLetterStatus xmasLetterStatus,
		CancellationToken cancellationToken = default)
	{
		var entity = XmasLetter.CreateXmasLetter(aggregateId, xmasLetterNumber, receivedOn, childEmail, letterSubject, letterBody, xmasLetterStatus);
		await Persister.InsertAsync(entity, cancellationToken);
	}
}