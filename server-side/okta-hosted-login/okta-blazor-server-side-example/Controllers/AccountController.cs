using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Okta.AspNetCore;

namespace okta_blazor_server_side_example.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public IActionResult SignIn([FromQuery] string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            return Challenge(OktaDefaults.MvcAuthenticationScheme);
        }

        public IActionResult SignOut([FromQuery] string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            return SignOut(
                new AuthenticationProperties() { RedirectUri = Url.Content("~/") },
                new[]
                {
                    OktaDefaults.MvcAuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                });
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
