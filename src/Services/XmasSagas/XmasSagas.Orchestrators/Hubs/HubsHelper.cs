using Microsoft.AspNetCore.SignalR;

namespace XmasSagas.Orchestrators.Hubs;

public class HubsHelper(IHubContext<XmasHub> hubContext) : IHubsHelper
{
	public async Task TellChildrenThatXmasSagaWasStarted(string message)
	{
		await hubContext.Clients.All.SendAsync("XmasSagaStarted", message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasApproved(string message)
	{
		await hubContext.Clients.All.SendAsync("XmasLetterApproved", message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasLetterWasProcessed(string message)
	{
		await hubContext.Clients.All.SendAsync("XmasLetterProcessed", message).ConfigureAwait(false);
	}

	public async Task TellChildrenThatXmasSagaWasCompleted(string message)
	{
		await hubContext.Clients.All.SendAsync("XmasSagaCompleted", message).ConfigureAwait(false);
	}
}