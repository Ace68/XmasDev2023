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
            From = "alberto.acerbis@gmail.com",
            Subject = "My Wish List",
            Body = XmasLetterContent
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