using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
		//endpoints.Use
		endpoints.MapHub<XmasHub>("/hubs/xmas", options =>
		{
			options.AllowStatefulReconnects = true;
		});

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

	//public static async Task<IResult> HandleSignalR(IHubContext<XmasHub, IHubsHelper> hubContext)
	//{
	//	await hubContext.Clients.All.TellChildrenThatXmasSagaWasStarted("Santa Claus", "Your xmasLetter has been Received");
	//	await hubContext.Clients.All.TellChildrenThatXmasLetterWasApproved("Santa Claus", "Your xmasLetter has been Approved");
	//	await hubContext.Clients.All.TellChildrenThatXmasLetterWasProcessed("Santa Claus", "Your xmasLetter has been Processed");
	//	await hubContext.Clients.All.TellChildrenThatXmasSagaWasCompleted("Santa Claus", "XmasSaga is completed");


	//	return Results.NoContent();
	//}

	public static async Task<IResult> HandleSignalR(IHubService hubService)
	{
		await hubService.TellChildrenThatXmasSagaWasStarted("Santa Claus", "Your xmasLetter has been Received");
		await hubService.TellChildrenThatXmasLetterWasApproved("Santa Claus", "Your xmasLetter has been Approved");
		await hubService.TellChildrenThatXmasLetterWasProcessed("Santa Claus", "Your xmasLetter has been Processed");
		await hubService.TellChildrenThatXmasSagaWasCompleted("Santa Claus", "XmasSaga is completed");


		return Results.NoContent();
	}
}