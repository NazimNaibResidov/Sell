using System.Threading.Tasks;

namespace WebApp.Application.Interfaces
{
    public interface IIdentityService
    {
        string GetUserName();
        string GetUserToken();
        bool IsLoggedIn { get; }
        Task<bool> Login(string userName,string password);
        void Logout();
    }
}
