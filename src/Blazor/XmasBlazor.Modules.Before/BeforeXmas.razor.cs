using BlazorComponentBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Modules.Before.Extensions.Messages;

namespace XmasBlazor.Modules.Before;

public class BeforeXmasBase : ComponentBase, IDisposable
{
	[Inject] private ComponentBus Bus { get; set; } = default!;
	[Inject] private NavigationManager NavigationManager { get; set; } = default!;

	protected XmasLetterJson XmasLetter { get; set; } = default!;

	protected IEnumerable<string> XmasLetterMessages { get; set; } = Enumerable.Empty<string>()!;

	private HubConnection? _hubConnection;
	protected string Message { get; set; } = string.Empty;


	protected override async Task OnInitializedAsync()
	{
		Bus.Subscribe<XmasLetterSubmitted>(XmasLetterSubmittedEventHandler);

		_hubConnection = new HubConnectionBuilder()
			.WithUrl(new Uri("https://localhost:44302/hubs/xmas"))
			.WithAutomaticReconnect()
			.Build();

		XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
		{
			"Waiting for SantaClaus connecting ..."
		});

		_hubConnection.On<string>("XmasHubConnected", UpdateXmasMessagesAsync);
		_hubConnection.On<string>("XmasLetterApproved", UpdateXmasMessagesAsync);
		_hubConnection.On<string>("XmasLetterProcessed", UpdateXmasMessagesAsync);
		_hubConnection.On<string>("XmasSagaCompleted", UpdateXmasMessagesAsync);

		await _hubConnection.StartAsync();

		await base.OnInitializedAsync();
	}

	private async Task UpdateXmasMessagesAsync(string message)
	{
		XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
		{
			message
		});

		await InvokeAsync(StateHasChanged);
	}

	private void XmasLetterSubmittedEventHandler(MessageArgs args)
	{
		var @event = args.GetMessage<XmasLetterSubmitted>();
		XmasLetter = @event.XmasLetter;
	}

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
			return;

		_hubConnection?.DisposeAsync();
	}

	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	~BeforeXmasBase()
	{
		Dispose(false);
	}
	#endregion
}