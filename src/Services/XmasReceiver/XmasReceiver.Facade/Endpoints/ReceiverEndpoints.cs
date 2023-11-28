using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using XmasReceiver.Facade.Validators;
using XmasReceiver.Shared.BindingContracts;

namespace XmasReceiver.Facade.Endpoints;

public static class ReceiverEndpoints
{
	public static IEndpointRouteBuilder MapReceiverEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/receivers/")
			.WithTags("Receivers");

		group.MapGet("xmasletters", HandleGetXmasLetters)
			.WithName("GetXmasLetters");

		group.MapPost("xmasletters", HandlePostXmasLetter)
			.WithName("PostXmasLetter");

		return endpoints;
	}

	public static async Task<IResult> HandleGetXmasLetters(IReceiverFacade receiverFacade,
		CancellationToken cancellationToken)
	{
		var xmasLettersResult = await receiverFacade.GetXmasLetterAsync(cancellationToken);

		return Results.Ok(xmasLettersResult.Results);
	}

	public static async Task<IResult> HandlePostXmasLetter(IReceiverFacade receiverFacade,
		IValidator<XmasLetterContract> validator,
		ValidationHandler validationHandler,
		XmasLetterContract body,
		CancellationToken cancellationToken)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		var xmasLetterId = await receiverFacade.PostXmasLetterAsync(body, cancellationToken);

		return Results.Ok(xmasLetterId);
	}
}