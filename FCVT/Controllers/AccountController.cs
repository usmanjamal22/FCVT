using FCVT.Interfaces;
using FCVT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCVT.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthen _authen;

        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, IAuthen authen)
        {
            _configuration = configuration;
            _logger = logger;
            _authen = authen;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.RecaptchaSiteKey = _configuration["GoogleReCaptcha:SiteKey"];
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var recaptchaResponse = Request.Form["g-recaptcha-response"];
            var isCaptchaValid = await IsReCaptchaValid(recaptchaResponse);
            if (!isCaptchaValid)
            {
                ModelState.AddModelError(string.Empty, "Please verify that you are not a robot.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View(model);

            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var user = await _authen.CheckUserCredential(model.UserName, ipAddress);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, user.Password, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim("UserName", user.UserName),
                    new Claim("UserType", user.UserType),
                    new Claim("UserID",user.Id)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(model);
        }

        private async Task<bool> IsReCaptchaValid(string token)
        {
            var secretKey = _configuration["GoogleReCaptcha:SecretKey"];
            using var client = new HttpClient();

            var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}", null);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var result = await response.Content.ReadAsStringAsync();
            dynamic jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            return jsonData.success == true;
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            ViewBag.RecaptchaSiteKey = _configuration["GoogleReCaptcha:SiteKey"];
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
