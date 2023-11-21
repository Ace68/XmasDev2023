using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace XmasBlazor.Modules;

public class IndexBase : ComponentBase, IAsyncDisposable
{
	private HubConnection HubConnection { get; set; } = default!;

	protected bool IsConnected => HubConnection.State == HubConnectionState.Connected;

	protected override async Task OnInitializedAsync()
	{
		// HubConnection = new HubConnectionBuilder()
		//     .WithUrl("http://localhost:5043/device")
		//     .WithAutomaticReconnect()
		//     .Build();

		// HubConnection.On<string, string>("deviceUpdateForAll", (clientId, message) =>
		// {
		//     Message = message;
		//
		//     Messages = Messages.Prepend($"{clientId}: {message}");
		//     StateHasChanged();
		// });

		// await HubConnection.StartAsync();
	}

	#region Dispose
	public async ValueTask DisposeAsync()
	{
		// await HubConnection.DisposeAsync();
	}
	#endregion
}