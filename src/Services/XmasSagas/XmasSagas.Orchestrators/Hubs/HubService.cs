using Microsoft.AspNetCore.SignalR;

namespace XmasSagas.Orchestrators.Hubs;

public sealed class HubService : IHubService
{
	public static IHubContext<XmasHub, IHubsHelper>? HubContext;

	public void RegisterHubContext(IHubContext<XmasHub, IHubsHelper> hubContext)
	{
		HubContext = hubContext;
	}

	public async Task TellChildrenThatClientIsConnected(string user, string message)
	{
		await HubContext.Clients.All.TellChildrenThatClientIsConnected(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatClientIsDisconnected(string user, string message)
	{
		await HubContext!.Clients.All.TellChildrenThatClientIsDisconnected(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasSagaWasStarted(string user, string message)
	{
		await HubContext!.Clients.All.TellChildrenThatXmasSagaWasStarted(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasApproved(string user, string message)
	{
		await HubContext!.Clients.All.TellChildrenThatXmasLetterWasApproved(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasProcessed(string user, string message)
	{
		await HubContext!.Clients.All.TellChildrenThatXmasLetterWasProcessed(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasSagaWasCompleted(string user, string message)
	{
		await HubContext!.Clients.All.TellChildrenThatXmasSagaWasCompleted(user, message);
	}
}