using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Shared.Configuration;

namespace XmasBlazor.Modules.Before.Extensions.Services;

public interface IXmasLetterService
{
	Task SendXmasLetterAsync(XmasLetterJson xmasLetter);
	Task<SignalRToken> GetAccessTokenAsync(string appConfigurationTokenNegotiateUri);
}