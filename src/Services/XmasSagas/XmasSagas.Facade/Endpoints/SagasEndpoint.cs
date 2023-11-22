using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace XmasSagas.Facade.Endpoints;

public static class SagasEndpoint
{
	public static IEndpointRouteBuilder MapSagasEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/sagas/")
			.WithTags("Sagas");

		group.MapGet("xmasletters", HandleSendXmasLetters)
			.WithName("SendXmasLetters");

		return endpoints;
	}

	public static async Task<IResult> HandleSendXmasLetters(
		CancellationToken cancellationToken)
	{
		return Results.Ok();
	}
}