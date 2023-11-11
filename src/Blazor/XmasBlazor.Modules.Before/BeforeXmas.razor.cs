using Microsoft.AspNetCore.Components;

namespace XmasBlazor.Modules.Before;

public class BeforeXmasBase : ComponentBase, IDisposable
{

	#region Dispose
	public void Dispose(bool disposing)
	{
		if (disposing)
		{
		}
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