using Microsoft.AspNetCore.SignalR;
using XmasSagas.Orchestrators.Hubs;

namespace XmasSagas.Middleware;

/// <summary>
/// 
/// </summary>
public class ResolveHubContextMiddleware
{
	private readonly RequestDelegate _next;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="next"></param>
	public ResolveHubContextMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <param name="hubService"></param>
	/// <returns></returns>
	public async Task InvokeAsync(HttpContext context,
		IHubService hubService)
	{
		var hubContext = context.RequestServices.GetService<IHubContext<XmasHub, IHubsHelper>>();
		hubService.RegisterHubContext(hubContext!);

		await _next(context);
	}
}
