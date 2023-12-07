﻿using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
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

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticationService.Authenticate(loginViewModel.Email, loginViewModel.Password);

            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Log In Attempt Failed. Pleace try again.");
            return View(loginViewModel);
        }
    }
}