using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RPizza.Shared;

namespace RPizza.Server.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserInfo LoggedOutUser =
         new UserInfo {IsAutenticated= false };



        [HttpGet("user")]
        public UserInfo GetUser()
        {
            return User.Identity.IsAuthenticated
            ? new UserInfo
            {
                Name = User.Identity.Name,IsAutenticated = true
            }
            : LoggedOutUser;
        }

        [HttpGet("user/signin")]
        public async Task SignIn(string redirectUri)
        {
            if (string.IsNullOrEmpty(redirectUri) ||
            !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/";
            }
            await HttpContext.ChallengeAsync(
             TwitterDefaults.AuthenticationScheme,
             new AuthenticationProperties
             {
                 RedirectUri = redirectUri
             });
        }

        [HttpGet("user/signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
