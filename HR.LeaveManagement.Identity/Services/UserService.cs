using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HR.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var emplyees = await _userManager.GetUsersInRoleAsync("Employee");
            return emplyees.Select(key => new Employee
            {
                Id = key.Id,
                Email = key.Email,
                FirstName = key.FirstName,
                LastName = key.LastName,
            }).ToList();
        }

        public async Task<Employee> GetEmployeeAsync(string userId)
        {
            ApplicationUser employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Id = employee.Id,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }
    }
}