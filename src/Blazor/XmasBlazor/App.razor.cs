using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using System.Reflection;

namespace XmasBlazor;

public class AppBase : ComponentBase, IDisposable
{
	[Inject] private LazyAssemblyLoader AssemblyLoader { get; set; } = default!;
	[Inject] private ILogger<App> Logger { get; set; } = default!;

	protected readonly List<Assembly> LazyLoadedAssemblies = new();

	protected async Task OnNavigateAsync(NavigationContext args)
	{
		try
		{
			switch (args.Path)
			{
				case "before":
					{
						var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
					{
						"XmasBlazor.Modules.Before.wasm"
					});
						LazyLoadedAssemblies.AddRange(assemblies);
						break;
					}

				case "after":
					{
						var assemblies = await AssemblyLoader.LoadAssembliesAsync(new List<string>
						{
							"XmasBlazor.Modules.After.wasm"
						});
						LazyLoadedAssemblies.AddRange(assemblies);
						break;
					}
			}
		}
		catch (Exception ex)
		{
			Logger.LogError($"Error Loading spares page: {ex}");
		}
	}

	#region Dispose
	public void Dispose(bool disposing)
	{
		if (disposing)
		{
		}
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~AppBase()
	{
		Dispose(false);
	}
	#endregion
}