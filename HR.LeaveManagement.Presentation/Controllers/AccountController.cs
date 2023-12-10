using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                loginViewModel.ReturnUrl ??= Url.Content("~/");
                var isLoggedIn = await _authenticationService.Authenticate(loginViewModel.Email, loginViewModel.Password);

                if (isLoggedIn)
                {
                    return LocalRedirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Log In Attempt Failed. Pleace try again.");
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isCreated = await _authenticationService.Register(registerViewModel);

                if (isCreated)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Registration Attempt Failed. Please try again");
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authenticationService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}