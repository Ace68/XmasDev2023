using Microsoft.AspNetCore.Components;

namespace XmasBlazor.Modules.After;

public class AfterXmasBase : ComponentBase, IDisposable
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

	~AfterXmasBase()
	{
		Dispose(false);
	}
	#endregion
}