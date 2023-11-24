using Microsoft.Extensions.Logging;
using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.ReadModel.Entities;
using XmasReceiver.Shared.BindingContracts;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.ReadModel.Services;

public sealed class XmasLetterService : BaseService, IXmasLetterService
{
	private readonly IQueries<XmasLetter> _queries;

	public XmasLetterService(IPersister persister, ILoggerFactory loggerFactory, IQueries<XmasLetter> queries) : base(persister, loggerFactory)
	{
		_queries = queries;
	}

	public async Task ReceiveLetterAsync(XmasLetterId aggregateId, XmasLetterNumber xmasLetterNumber, ReceivedOn receivedOn,
		ChildEmail childEmail, LetterSubject letterSubject, LetterBody letterBody, XmasLetterStatus xmasLetterStatus,
		CancellationToken cancellationToken = default)
	{
		var entity = await Persister.GetByIdAsync<XmasLetter>(aggregateId.Value.ToString(), cancellationToken);
		if (entity != null && !string.IsNullOrWhiteSpace(entity.XmasLetterNumber))
			return;

		entity = XmasLetter.CreateXmasLetter(aggregateId, xmasLetterNumber, receivedOn, childEmail, letterSubject, letterBody, xmasLetterStatus);
		await Persister.InsertAsync(entity, cancellationToken);
	}

	public async Task<PagedResult<XmasLetterContract>> GetXmasLetterAsync(CancellationToken cancellationToken)
	{
		var results = await _queries.GetByFilterAsync(null, 0, 200, cancellationToken);

		return new PagedResult<XmasLetterContract>(results.Results.Select(r => r.ToJson()), results.Page,
			results.PageSize, results.TotalRecords);
	}
}