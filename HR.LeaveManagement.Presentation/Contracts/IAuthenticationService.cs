using HR.LeaveManagement.Presentation.Models;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(LoginViewModel loginViewModel);

        Task<bool> Register(RegisterViewModel registerViewModel);

        Task Logout();
    }
}