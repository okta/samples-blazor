# Blazor Sample Applications for Okta

This repository contains several sample applications that demonstrate how to integrate various Okta use-cases into your Blazor applications.

Please find the sample that fits your use-case from the table below.


| Sample | Description | Use-Case |
|--------|-------------|----------|
| [Blazor Server-Side Okta-Hosted Login](/server-side/okta-hosted-login) | A Blazor server-side application that uses the hosted login page on your Okta org, then creates a cookie session for the user in the Blazor application. | Traditional web applications with server-side rendered pages. |
| [Blazor WebAssembly Okta-Hosted Login](/web-assembly/okta-hosted-login) |  A Blazor WebAssembly application that uses Okta to authenticate users via the hosted login page on your Okta org and call API calls. This sample includes a WASM client application. It's intended to be used in conjunction with one of our resource servers, for example [AspNet Core 3.x Samples Resource Server](https://github.com/okta/samples-aspnetcore/tree/master/samples-aspnetcore-3x/resource-server). This demonstrates how to authenticate requests with access tokens that have been issued by Okta. | Single-Page applications. |


## Contributing
 
We're happy to accept contributions and PRs! Please see the [contribution guide](CONTRIBUTING.md) to understand how to structure a contribution.
