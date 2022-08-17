using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Extensions;

namespace WebApp.Utils
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = httpClient;
            this.localStorageService = syncLocalStorageService;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string apiToken = await localStorageService.GetToke();
            if (string.IsNullOrEmpty(apiToken))
                return anonymous;
            string userName = await localStorageService.GetUserName();
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
           {
                new Claim(ClaimTypes.Name, userName),
            }, "jwtAuthType"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", apiToken);
            return new AuthenticationState(cp);
        }

        public void NotifyUserLogin(String userName)
        {
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
            }));
        }

        public void NotifyUserLoginOut()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}