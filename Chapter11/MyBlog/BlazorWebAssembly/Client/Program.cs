using Blazored.SessionStorage;
using BlazorWebAssembly.Client;
using BlazorWebAssembly.Client.Services;
using Components.Interfaces;
using Components.RazorComponents;
using Data;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//<Chapter8>
builder.Services.AddHttpClient("Public",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddHttpClient("Authenticated",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
  .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Authenticated"));

builder.Services.AddTransient<ILoginStatus, LoginStatusWasm>();
builder.Services.AddTransient<IBlogApi, BlogApiWebClient>();
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";

    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);

}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();
//<Chapter8>
//<Chapter11>
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IBrowserStorage, BlogBrowserStorage>();
builder.Services.AddSingleton<IBlogNotificationService, BlazorWebAssemblyBlogNotificationService>();
//</Chapter11>

await builder.Build().RunAsync();
