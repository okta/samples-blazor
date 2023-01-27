using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using okta_blazor_web_assembly_example;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// AddHttpClient is an extension in Microsoft.Extensions.Http
builder.Services.AddHttpClient("BlazorClient.ServerApi",
        client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServerApi:BaseAddress")))
    .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { builder.Configuration.GetValue<string>("ServerApi:BaseAddress") }));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("BlazorClient.ServerApi"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Okta", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
