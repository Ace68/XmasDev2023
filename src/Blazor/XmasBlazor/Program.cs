using BlazorComponentBus;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using XmasBlazor;
using XmasBlazor.Modules.Before.Extensions;
using XmasBlazor.Shared.Configuration;
using XmasBlazor.Shared.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Configuration
builder.Services.AddSingleton(_ => builder.Configuration.GetSection("XmasBlazor:AppConfiguration")
	.Get<AppConfiguration>());
builder.Services.AddApplicationService();
#endregion

builder.Services.AddMudServices();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<ComponentBus>();

#region Modules
builder.Services.AddXmasBlazorBefore();
#endregion

await builder.Build().RunAsync();
