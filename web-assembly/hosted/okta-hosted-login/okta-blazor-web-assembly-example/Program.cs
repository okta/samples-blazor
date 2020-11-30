using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace okta_blazor_web_assembly_example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //- For cross-domain requests to Api Server:
            builder.Services.AddScoped<CorsRequestAuthorizationMessageHandler>(); 
            builder.Services
                .AddHttpClient("BlazorClient.ServerApi", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServerApi:BaseAddress")))
                .AddHttpMessageHandler<CorsRequestAuthorizationMessageHandler>();
            
            //- Otherwise use the following instead:
            //builder.Services
            //    .AddHttpClient("BlazorClient.ServerApi", client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServerApi:BaseAddress")))
            //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorClient.ServerApi"));

            builder.Services.AddOidcAuthentication(options =>
            {
                // Replace the Okta placeholders with your Okta values in the appsettings.json file.
                options.ProviderOptions.Authority = builder.Configuration.GetValue<string>("Okta:Authority");
                options.ProviderOptions.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId"); ;
                options.ProviderOptions.ResponseType = "code";
            });

            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
