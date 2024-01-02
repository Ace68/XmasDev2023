using BlazorComponentBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Modules.Before.Extensions.Messages;
using XmasBlazor.Modules.Before.Extensions.Services;
using XmasBlazor.Shared.Configuration;

namespace XmasBlazor.Modules.Before;

public class BeforeXmasBase : ComponentBase, IAsyncDisposable
{
	[Inject] private IXmasLetterService XmasLetterService { get; set; } = default!;
	[Inject] private ComponentBus Bus { get; set; } = default!;
	[Inject] private NavigationManager NavigationManager { get; set; } = default!;
	[Inject] private AppConfiguration AppConfiguration { get; set; } = default!;

	protected XmasLetterJson XmasLetter { get; set; } = default!;

	protected IEnumerable<string> XmasLetterMessages { get; set; } = Enumerable.Empty<string>()!;

	private HubConnection? _hubConnection;
	protected string Message { get; set; } = string.Empty;


	protected override async Task OnInitializedAsync()
	{
		Bus.Subscribe<XmasLetterSubmitted>(XmasLetterSubmittedEventHandler);

		var signalRUri = new Uri(AppConfiguration.SignalRUri);

		_hubConnection = new HubConnectionBuilder()
			.WithUrl(signalRUri, options =>
			{
				options.AccessTokenProvider = async () =>
				{
					var token = await XmasLetterService.GetAccessTokenAsync(AppConfiguration.TokenNegotiateUri);
					return token.AccessToken;
				};
			})
			.WithServerTimeout(TimeSpan.FromSeconds(60))
			.WithKeepAliveInterval(TimeSpan.FromSeconds(15))
			.WithAutomaticReconnect()
			.Build();

		XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
		{
			"Waiting for SantaClaus connecting ..."
		});

		_hubConnection.On<string, string>("TellChildrenThatClientIsConnected", UpdateXmasMessagesAsync);

		_hubConnection.On<string, string>("TellChildrenThatXmasSagaWasStarted", UpdateXmasMessagesAsync);
		_hubConnection.On<string>("TellChildrenThatXmasSagaWasStarted", UpdateXmasMessagesAsync);
		_hubConnection.On<string, string>("TellChildrenThatXmasLetterWasApproved", UpdateXmasMessagesAsync);
		_hubConnection.On<string, string>("TellChildrenThatXmasLetterWasProcessed", UpdateXmasMessagesAsync);
		_hubConnection.On<string, string>("TellChildrenThatXmasSagaWasCompleted", UpdateXmasMessagesAsync);

		await _hubConnection.StartAsync();

		if (_hubConnection.State == HubConnectionState.Connected)
		{
			XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
			{
				"SantaClaus is connected."
			});
		}

		await base.OnInitializedAsync();
	}

	private async Task UpdateXmasMessagesAsync(string target, string message)
	{
		if (string.IsNullOrWhiteSpace(message))
			message = "No Message";

		XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
		{
			message
		});

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateXmasMessagesAsync(string message)
	{
		if (string.IsNullOrWhiteSpace(message))
			message = "No Message";

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

	protected async Task SendXmasLetterAsync()
	{
		XmasLetter = new XmasLetterJson
		{
			XmasLetterNumber = $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}",
			ReceivedOn = DateTime.UtcNow,
			ChildEmail = "child@xmas.com",
			LetterSubject = "My Wishes",
			LetterBody = "I wish a new bike and a new computer."
		};
		await XmasLetterService.SendXmasLetterAsync(XmasLetter);
	}

	#region Dispose
	private void ReleaseUnmanagedResources()
	{
		// TODO release unmanaged resources here
	}

	private async ValueTask DisposeAsyncCore()
	{
		ReleaseUnmanagedResources();

		if (_hubConnection != null) await _hubConnection.DisposeAsync();
	}

	public async ValueTask DisposeAsync()
	{
		await DisposeAsyncCore();
		GC.SuppressFinalize(this);
	}
	#endregion
}