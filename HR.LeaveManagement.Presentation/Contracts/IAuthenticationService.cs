using HR.LeaveManagement.Presentation.Models.Accounts;

namespace HR.LeaveManagement.Presentation.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string Password);

        Task<bool> Register(RegisterViewModel registerViewModel);

        Task Logout();
    }
}