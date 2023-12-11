using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using XmasSagas.Facade.Validators;
using XmasSagas.Orchestrators.Hubs;
using XmasSagas.Shared.BindingContracts;

namespace XmasSagas.Facade.Endpoints;

public static class SagasEndpoint
{
	public static IEndpointRouteBuilder MapSagasEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/sagas/")
			.WithTags("Sagas");

		group.MapPost("xmasletters", HandleSendXmasLetters)
			.WithName("SendXmasLetters");

		group.MapPost("broadcast", HandleSignalR)
			.WithName("SignalR");

		return endpoints;
	}

	public static IEndpointRouteBuilder MapSignalR(this IEndpointRouteBuilder endpoints)
	{
		endpoints.MapHub<XmasHub>("/hubs/xmas");

		return endpoints;
	}

	public static async Task<IResult> HandleSendXmasLetters(
		ISagasFacade sagasFacade,
		IValidator<XmasLetterContract> validator,
		ValidationHandler validationHandler,
		XmasLetterContract body,
		CancellationToken cancellationToken)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		await sagasFacade.SendXmasLettersAsync(body, cancellationToken);
		return Results.Ok();
	}

	public static async Task<IResult> HandleSignalR(IHubContext<XmasHub, IHubsHelper> hubContext)
	{
		await hubContext.Clients.All.TellChildrenThatXmasSagaWasStarted("Santa Claus", "Your xmasLetter has been Received");

		return Results.NoContent();
	}
}