using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PoleTimeGuesser.Library.Models;
using PoleTimeGuesser.Web;
using PoleTimeGuesser.Web.ViewModel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ISharedData, SharedData>();
builder.Services.AddScoped<IIndexViewModel, IndexViewModel>();

await builder.Build().RunAsync();
