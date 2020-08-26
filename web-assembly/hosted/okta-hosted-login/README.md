# Hosted Blazor WebAssembly & Okta-Hosted Sign-In Page Example

This example demonstrates a hosted Blazor WebAssembly application that uses Okta to authenticate users and API calls.

When the user clicks `Sign In` their browser is first redirected to the Okta-hosted sign-in page. After the user authenticates, they are redirected back to your application. Blazor automatically populates `AuthenticationState.User` with the information Okta sends back about the user. 

Once the user is authenticated, they can access protected resources in your Server. In this example, if the user navigates to `/fetchdata` a `GET /WeatherForecast` request is made to fetch the data populates the page. The access token is added to the request and validated by the Server. 


## Prerequisites

Before running this example, you will need the following:

* An Okta Developer Account, you can sign up for one at https://developer.okta.com/signup/.
* An Okta Application, configured for Single-Page App (SPA) mode. This is done from the Okta Developer Console, see the [Create an Okta SPA application](#create-an-okta-spa-application) section for step-by-step instructions. When following the wizard, use the default properties. They are are designed to work with our sample applications.

## Create an Okta SPA application

This section describes the steps necessary to create an Okta SPA application from the Okta developer console. Sign into your developer console and perform the following steps:

1. Select Applications, then Add Application. 

1. Pick Single-Page App (SPA) as the platform. 

1. Enter a name for your application (or leave the default value).

1. Add the Base URI of your application during local development, such as https://localhost:44314. Also, add any base URIs where your application runs in production, such as https://app.example.com.

1. Enter values for the Login redirect URI. Add values for local development (such as https://localhost:44314/authentication/login-callback) and production (such as https://app.example.com/authentication/login-callback).

1. Enter values for the Logout redirect URI. Add values for local development (such as https://localhost:44314/authentication/logout-callback) and production (such as https://app.example.com/authentication/logout-callback).

1. For Allowed grant types, ensure "Authorization Code" is checked. This enables PKCE flow for your application.

1. Click Done to finish creating the Okta Application. You need to copy your [Okta domain](https://developer.okta.com/docs/guides/find-your-domain/findorg/) and Client Id values into your code later, so leave the Developer Console open.

## Running This Example

This section describes the available options for running this example.

### Clone this repository

```git clone https://github.com/okta/samples-blazor.git```

### Run the web application

Run the example with your preferred tool and write down the port of your web application to configure Okta afterwards.

> **NOTE:** This sample is using ASP.NET Core 3.1 which enforces HTTPS. This is a recommended practice for web applications. Check out [Enforce HTTPS in ASP.NET Core] for more details.

#### Run the web application from Visual Studio

If you run this project in Visual Studio it will start the web application on port 44314 using HTTPS. You can change this configuration in the `launchSettings.json`. Make sure that `okta-blazor-web-assembly-example.Server` is selected as your startup project.

#### Run the web application from dotnet CLI

If you run this project via the dotnet CLI it will start the web application on port 5001 using HTTPS. You can change this configuration in the `launchSettings.json`. 

Navigate to the folder where the project file is located and type the following:

```dotnet run```

#### Trust the local dev certificate if necessary

If you’ve never run an ASP.NET Core 3.x application before, you may notice a strange error page come up warning you that the site is potentially unsafe.
This is because ASP.NET Core creates an HTTPS development certificate for you as part of the first-run experience, but it still needs to be trusted. You can ignore the warning by clicking on Advanced and telling the browser that it’s okay to visit this site even though there is no certificate for it. Or you can trust the certificate to get rid of this warning, check out [Configuring HTTPS in ASP.NET Core across different platforms] for more details.

### Add your Okta configuration to the sample's appsettings

#### Client

Open the `appsettings.json` file located in the `wwwroot` folder and replace the Okta configuration placeholders with your with your configuration values from the [Okta Developer Console].

#### Server

Open the `appsettings.json` and replace the Okta configuration placeholders with your with your configuration values from the [Okta Developer Console].

### Run again and try to sign in

Click the **Sign In** link in the Home page and it will redirect you to the Okta hosted sign-in page.

You can sign in with the same account that you created when signing up for your Developer Org, or you can use a known username and password from your Okta Directory.

**Note:** If you are currently using your Developer Console, you already have a Single Sign-On (SSO) session for your Org.  You will be automatically signed into your application as the same user that is using the Developer Console.  You may want to use an incognito tab to test the flow from a blank slate.

[Okta ASP.NET Core SDK]: https://github.com/okta/okta-aspnet
[OIDC Web Application Setup Instructions]: https://developer.okta.com/authentication-guide/implementing-authentication/auth-code#1-setting-up-your-application
[Enforce HTTPS in ASP.NET Core]: https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2&tabs=visual-studio
[Configuring HTTPS in ASP.NET Core across different platforms]:https://devblogs.microsoft.com/aspnet/configuring-https-in-asp-net-core-across-different-platforms/
[Okta Developer Console]: https://login.okta.com