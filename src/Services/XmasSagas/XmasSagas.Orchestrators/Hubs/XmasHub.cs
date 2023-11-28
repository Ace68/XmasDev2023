using Microsoft.AspNetCore.SignalR;

namespace XmasSagas.Orchestrators.Hubs;

public class XmasHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("XmasHubConnected", "SantaClaus is Connected").ConfigureAwait(false);
		await Clients.All.SendAsync("XmasLetterApproved", "Waiting for xmasLetter").ConfigureAwait(false);

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("XmasHubDisconnected", "XmasHub Disconnected").ConfigureAwait(false);

		await base.OnDisconnectedAsync(exception);
	}
}