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

		group.MapGet("xmasletters", HandleGetXmasLetters)
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("GetXmasLetters");

		return endpoints;
	}

	public static async Task<IResult> HandleGetXmasLetters(IReceiverFacade receiverFacade,
		CancellationToken cancellationToken)
	{
		var xmasLettersResult = await receiverFacade.GetXmasLetterAsync(cancellationToken);

		return Results.Ok(xmasLettersResult.Results);
	}
}