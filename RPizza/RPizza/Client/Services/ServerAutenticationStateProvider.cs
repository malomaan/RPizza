using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
namespace RPizza.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var Claim = new Claim(ClaimTypes.Name, "Usuario False");
            var Identity = new ClaimsIdentity(new[] { Claim }, "serverauth");
            return new AuthenticationState(new ClaimsPrincipal(Identity));
        }
    }

}
