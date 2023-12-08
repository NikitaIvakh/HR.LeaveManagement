using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.Presentation.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IMapper _mapper;

        public AuthenticationService(ILocalStorageService localStorageService, IClient client, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(localStorageService, client)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _mapper = mapper;
        }

        public async Task<bool> Authenticate(LoginViewModel loginViewModel)
        {
            try
            {
                AuthRequest authRequest = _mapper.Map<AuthRequest>(loginViewModel);
                AuthResponse authenticateResposne = await _client.LoginAsync(authRequest);

                if (authenticateResposne.Token != string.Empty)
                {
                    var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authenticateResposne.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue("token", authenticateResposne.Token);

                    return true;
                }

                return false;
            }

            catch
            {
                return false;
            }
        }

        public async Task<bool> Register(RegisterViewModel registerViewModel)
        {
            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registerViewModel);
            RegistrationResponse response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private static IEnumerable<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

            return claims;
        }
    }
}