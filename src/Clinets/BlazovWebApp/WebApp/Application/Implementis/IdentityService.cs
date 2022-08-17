using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.Application.Interfaces;
using WebApp.Domain.Models;
using WebApp.Extensions;
using WebApp.Utils;

namespace WebApp.Application.Implementis
{
    public class IdentityService : IIdentityService
    {
        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        private readonly HttpClient client;
        private readonly ISyncLocalStorageService syncLocalStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            this.syncLocalStorageService = syncLocalStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public string GetUserName()
        {
            return syncLocalStorageService.GetUserName();
        }

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToke();
        }

        public async Task<bool> Login(string userName, string password)
        {
            var req = new UserLoginRequest(userName, password);
            var response = await client.PostGetResponseAsync<UserLoginResponse, UserLoginRequest>("auth", req);
            if (!String.IsNullOrEmpty(response.UserToken))
            {
                syncLocalStorageService.SetToken(response.UserToken);
                syncLocalStorageService.SetUserName(response.UserName);
                ((AuthStateProvider)authenticationStateProvider).NotifyUserLogin(response.UserName);
                var auther = new AuthenticationHeaderValue("Bearer", response.UserToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem("token");
            syncLocalStorageService.RemoveItem("username");
            ((AuthStateProvider)authenticationStateProvider).NotifyUserLoginOut();
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}