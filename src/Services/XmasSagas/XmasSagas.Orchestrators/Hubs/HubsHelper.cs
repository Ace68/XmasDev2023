using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace XmasSagas.Orchestrators.Hubs;

public class HubsHelper(IHubContext<XmasHub> hubContext, IServiceProvider serviceProvider) : IHubsHelper
{
	public async Task TellChildrenThatXmasLetterWasApproved(string message)
	{
		var hubContext = serviceProvider.GetRequiredService<IHubContext<XmasHub>>();
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