using Microsoft.AspNetCore.SignalR;

namespace XmasSagas.Orchestrators.Hubs;

public class XmasHub : Hub<IHubsHelper>
{
	public override async Task OnConnectedAsync()
	{
		await Clients.All.TellChildrenThatClientIsConnected("Santa Claus", "SantaClaus is Connected").ConfigureAwait(false);
		await Clients.All.TellChildrenThatClientIsConnected("Santa Claus", "Waiting for xmasLetter").ConfigureAwait(false);

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.TellChildrenThatClientIsDisconnected("Santa Claus", "XmasHub Disconnected").ConfigureAwait(false);

		await base.OnDisconnectedAsync(exception);
	}
}