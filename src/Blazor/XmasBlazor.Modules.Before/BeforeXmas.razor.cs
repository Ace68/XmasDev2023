﻿using BlazorComponentBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Modules.Before.Extensions.Messages;
using XmasBlazor.Modules.Before.Extensions.Services;
using XmasBlazor.Shared.Configuration;

namespace XmasBlazor.Modules.Before;

public class BeforeXmasBase : ComponentBase, IDisposable
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

		_hubConnection = new HubConnectionBuilder()
			.WithUrl(new Uri(AppConfiguration.SignalRUri))
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