using facebook.Models;

namespace facebook.Services
{
    public interface ILoginService
    {
        List<LoginViewModel> GetLogins();
        int Add(LoginViewModel request);
    }
}