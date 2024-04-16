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

        // Workaround for bug #260
        OpenIdConnectEvents = new OpenIdConnectEvents()
        {
            OnTokenResponseReceived = async context =>
            {
                // Make our own call to Okta.AspNet.Abstractions.StrictTokenHandler
                // First setup the parameterization as per OpenIdConnectHandler.cs:
                var _configuration = context.Options.Configuration;
                TokenValidationParameters validationParameters = context.Options.TokenValidationParameters.Clone();
                validationParameters.RequireSignedTokens = false;
                if (context.Options.ConfigurationManager is BaseConfigurationManager configurationManager)
                {
                    validationParameters.ConfigurationManager = configurationManager;
                }
                else if (_configuration != null)
                {
                    string[] array = new string[1] { _configuration.Issuer };
                    validationParameters.ValidIssuers = validationParameters.ValidIssuers?.Concat(array) ?? array;
                    validationParameters.IssuerSigningKeys = validationParameters.IssuerSigningKeys?.Concat(_configuration.SigningKeys) ?? _configuration.SigningKeys;
                }
                string idToken = context.TokenEndpointResponse.IdToken;

                // Call the validation and check
                TokenValidationResult tokenValidationResult = await context.Options.TokenHandler.ValidateTokenAsync(idToken, validationParameters);
                if (tokenValidationResult.Exception != null)
                {
                    throw tokenValidationResult.Exception;
                }

                // Since we have already done the validation, avoid doing again with the net8.0 code
                // which throws because of Okta's use of JwtSecurityToken instead of JsonWebToken:
                context.Options.UseSecurityTokenValidator = true;
            }
        },
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
