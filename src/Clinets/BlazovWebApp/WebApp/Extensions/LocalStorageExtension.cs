using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
    public static class LocalStorageExtension
    {
        public static string GetUserName(this ISyncLocalStorageService service)
        {
            return service.GetItem<string>("username");
        }

        public static async Task<string> GetUserName(this ILocalStorageService service)
        {
            return await service.GetItemAsync<string>("username");
        }

        public static void SetUserName(this ISyncLocalStorageService service, string value)
        {
            service.SetItem("username", value);
        }

        public static async Task SetUserName(this ILocalStorageService service, string value)
        {
            await service.SetItemAsync("username", value);
        }
        public static string GetToke(this ISyncLocalStorageService service)
        {
            return service.GetItem<string>("token");
        }

        public static async Task<string> GetToke(this ILocalStorageService service)
        {
            return await service.GetItemAsync<string>("token");
        }

        public static void SetToken(this ISyncLocalStorageService service, string value)
        {
            service.SetItem("token", value);
        }

        public static async Task SetToken(this ILocalStorageService service, string value)
        {
            await service.SetItemAsync("token", value);
        }
    }
}