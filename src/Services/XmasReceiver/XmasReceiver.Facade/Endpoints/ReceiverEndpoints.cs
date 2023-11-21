using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace XmasReceiver.Facade.Endpoints;

public static class ReceiverEndpoints
{
	public static IEndpointRouteBuilder MapReceiverEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/receivers/")
			.WithTags("Receivers");

		return endpoints;
	}

	public static async Task<IResult> HandleReceiveLetter(
		CancellationToken cancellationToken)
	{
		return Results.Ok();
	}
}