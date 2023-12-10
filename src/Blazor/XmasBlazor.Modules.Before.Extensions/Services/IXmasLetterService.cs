using XmasBlazor.Modules.Before.Extensions.Contracts;

namespace XmasBlazor.Modules.Before.Extensions.Services;

public interface IXmasLetterService
{
	Task SendXmasLetterAsync(XmasLetterJson xmasLetter);
}