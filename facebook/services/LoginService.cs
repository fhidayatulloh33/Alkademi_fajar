using facebook.Models;

namespace facebook.Services
{
    public class LoginService : ILoginService
    {
        List<Login> _logins;
        public LoginService()
        {
            _logins = new List<Login>
            {
                new Login("Fajar", "123"),
            };
        }
        public int Add(LoginViewModel request)
        {
            throw new NotImplementedException();
        }

        public List<LoginViewModel> GetLogins()
        {
            List<LoginViewModel> loginViewModels = new List<LoginViewModel>();

            foreach (var item in _logins)
            {
                loginViewModels.Add(new LoginViewModel(item.Username, item.Password));
            }

            return loginViewModels;
        }
    }
}
