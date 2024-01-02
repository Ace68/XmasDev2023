using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Shared.Abstracts;
using XmasBlazor.Shared.Configuration;

namespace XmasBlazor.Modules.Before.Extensions.Services;

public sealed class XmasLetterService(IHttpService httpService,
		AppConfiguration appConfiguration) : IXmasLetterService
{
	public async Task SendXmasLetterAsync(XmasLetterJson xmasLetter)
	{
		await httpService.Post($"{appConfiguration.XmasLetterApiUri}v1/sagas/xmasletters", xmasLetter);
	}

	public async Task<SignalRToken> GetAccessTokenAsync(string appConfigurationTokenNegotiateUri)
	{
		return await httpService.NegotiateSignalrTokenAsync(appConfigurationTokenNegotiateUri);
	}
}