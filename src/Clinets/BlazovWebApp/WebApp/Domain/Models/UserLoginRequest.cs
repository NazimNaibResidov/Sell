namespace WebApp.Domain.Models
{
    public class UserLoginRequest
    {
        public UserLoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string UserName { get; set; }
        public string UserToken { get; set; }
    }
}