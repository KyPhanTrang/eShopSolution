using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _config;

        public UserController(IUserApiClient userApiClient, IConfiguration config)
        {
            _userApiClient = userApiClient;
            _config = config;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 1)
        {
            var session = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(session)) return RedirectToAction("Login", "User");

            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _userApiClient.GetUsersPaging(request);

            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var result = await _userApiClient.Authenticate(loginRequest);
            var usePrincipal = this.ValidateToken(result.ResultObj);

            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = loginRequest.RememberMe,
                ExpiresUtc = loginRequest.RememberMe
                    ? DateTimeOffset.UtcNow.AddMinutes(30)
                    : DateTimeOffset.UtcNow.AddMinutes(15)
            };

            HttpContext.Session.SetString("Token", result.ResultObj);

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    usePrincipal,
                    authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.RegisterUser(registerRequest);
            if (result.IsSuccess) return RedirectToAction("Index", "User");

            ModelState.AddModelError("", result.Message);
            return View(registerRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);

            if (result.IsSuccess)
            {
                var userUpdateRequest = new UserUpdateRequest()
                {
                    Id = result.ResultObj.Id,
                    LastName = result.ResultObj.LastName,
                    FirstName = result.ResultObj.FirstName,
                    Email = result.ResultObj.Email,
                    Dob = result.ResultObj.Dob,
                    PhoneNumber = result.ResultObj.PhoneNumber
                };

                return View(userUpdateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest userUpdateRequest)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(userUpdateRequest.Id, userUpdateRequest);

            if (result.IsSuccess) return RedirectToAction("Index", "User");

            ModelState.AddModelError("", result.Message);
            return View(userUpdateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "User");
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;

            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidAudience = _config["Tokens:Issuer"],
                ValidIssuer = _config["Tokens:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
    }
}