using okta_blazor_server_side_example.Components;
using Okta.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Okta Auth:
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOktaMvc(new OktaMvcOptions
    {
        // Replace the Okta placeholders in appsettings.json with your Okta configuration.
        OktaDomain = builder.Configuration.GetValue<string>("Okta:OktaDomain"),
        ClientId = builder.Configuration.GetValue<string>("Okta:ClientId"),
        ClientSecret = builder.Configuration.GetValue<string>("Okta:ClientSecret"),
        AuthorizationServerId = builder.Configuration.GetValue<string>("Okta:AuthorizationServerId"),
    });
// To pick up AccountController:
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Add AuthN and AuthR, configured to Okta above:
app.UseAuthentication();
app.UseAuthorization();
// To pick up AccountController:
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
