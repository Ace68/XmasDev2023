using BlazorComponentBus;
using Microsoft.AspNetCore.Components;
using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Modules.Before.Extensions.Messages;

namespace XmasBlazor.Modules.Before;

public class BeforeXmasBase : ComponentBase, IDisposable
{
	[Inject] private ComponentBus Bus { get; set; } = default!;
	protected XmasLetterJson XmasLetter { get; set; } = default!;
	
	protected override Task OnInitializedAsync()
	{
		Bus.Subscribe<XmasLetterSubmitted>(XmasLetterSubmittedEventHandler);
		
		return base.OnInitializedAsync();
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