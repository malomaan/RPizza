using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using RPizza.Shared;
using System.Net.Http.Json;

namespace RPizza.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient HttpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var UserInfo = await HttpClient.GetFromJsonAsync<UserInfo>("user");
            var Identity = UserInfo.IsAutenticated ?
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name,UserInfo.Name)
                    }, "serverauth")
                : new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(Identity));
        }
    }

}
