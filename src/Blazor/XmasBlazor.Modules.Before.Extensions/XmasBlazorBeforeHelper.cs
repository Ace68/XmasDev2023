using Microsoft.Extensions.DependencyInjection;
using XmasBlazor.Modules.Before.Extensions.Services;

namespace XmasBlazor.Modules.Before.Extensions;

public static class XmasBlazorBeforeHelper
{
	public static IServiceCollection AddXmasBlazorBefore(this IServiceCollection services)
	{
		services.AddScoped<IXmasLetterService, XmasLetterService>();

		return services;
	}
}