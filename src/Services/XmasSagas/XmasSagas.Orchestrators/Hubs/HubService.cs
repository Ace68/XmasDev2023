using Microsoft.AspNetCore.SignalR;

namespace XmasSagas.Orchestrators.Hubs;

public sealed class HubService(IHubContext<XmasHub, IHubsHelper> hubContext) : IHubService
{
	public async Task TellChildrenThatClientIsConnected(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatClientIsConnected(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatClientIsDisconnected(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatClientIsDisconnected(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasSagaWasStarted(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatXmasSagaWasStarted(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasApproved(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatXmasLetterWasApproved(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasProcessed(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatXmasLetterWasProcessed(user, message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasSagaWasCompleted(string user, string message)
	{
		await hubContext.Clients.All.TellChildrenThatXmasSagaWasCompleted(user, message);
	}
}