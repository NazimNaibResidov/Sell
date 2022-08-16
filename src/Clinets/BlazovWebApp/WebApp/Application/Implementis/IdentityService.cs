using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Application.Interfaces;
using WebApp.Extensions;

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

        public Task<bool> Login(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}
