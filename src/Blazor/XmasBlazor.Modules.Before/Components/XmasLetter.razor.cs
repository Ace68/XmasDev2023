using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using XmasBlazor.Modules.Before.Extensions.Contracts;
using XmasBlazor.Modules.Before.Extensions.Messages;

namespace XmasBlazor.Modules.Before.Components;

public class XmasLetterBase : ComponentBase, IDisposable
{
	[Inject] private BlazorComponentBus.ComponentBus Bus { get; set; } = default!;
	protected BlazoredTextEditor TextEditor { get; set; } = default!;
	protected string XmasLetterContent { get; set; } = string.Empty;

	protected async Task SendLetterAsync()
	{
		XmasLetterContent = await TextEditor.GetText();
		await Bus.Publish(new XmasLetterSubmitted(new XmasLetterJson
		{
			XmasLetterNumber = $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}",
			ReceivedOn = DateTime.UtcNow,
			ChildEmail = "child@xmas.com",
			LetterSubject = "My Wishes",
			LetterBody = "I wish a new bike and a new computer."
		}));

		StateHasChanged();
	}

	public async Task SetHTML()
	{
		string QuillContent =
			@"<a href='http://BlazorHelpWebsite.com/'>" +
			"<img src='images/BlazorHelpWebsite.gif' /></a>";

		await TextEditor.LoadHTMLContent(QuillContent);
		StateHasChanged();
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

	~XmasLetterBase()
	{
		Dispose(false);
	}
	#endregion
}