using ePizzaHub.UI.Models.ApiModels.Response;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ePizzaHub.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            #region --Scenario--            
            //If below lines i.e. Step1 don't wants to add it in a Program.cs then Step2 needs to write 
            //Step1
            //builder.Services.AddHttpClient("ePizzaAPI", options =>
            //{
            //    options.BaseAddress = new Uri(builder.Configuration["EPizzaAPI:Url"]!);
            //    options.DefaultRequestHeaders.Add("Accept", "application/json");
            //});

            //Step2
            //using var client = new HttpClient();
            //client.BaseAddress = new Uri();
            //client.DefaultRequestHeaders = "";
            #endregion

            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("ePizzaAPI");
                var userDetails = await client.GetFromJsonAsync<ValidateUserResponse>
                    ($"Auth?userName={request.EmailAddress}&password={request.Password}");
                if (userDetails is not null)
                {
                    List<Claim> claims = [new Claim(ClaimTypes.Name, "Sample@123")];
                    //List<Claim> claims = [new Claim(ClaimTypes.Name, request.EmailAddress)];

                    await GenerateTicket(claims);
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel request)
        {
            //var client = httpClientFactory.CreateClient("ePizzaAPI");
            //var userDetails = await client.GetFromJsonAsync<ValidateUserResponse>
            //    ($"User?{request}");
            ////http://localhost:5241/api/User
            return View();

        }

        #region -- GenerateTicket - Explain --
        //https://chatgpt.com/c/6884def6-bcc8-8001-8672-bb4513bf3e6b
        //If the user is correct , place the data into my database
        //Right now doing a cookie based authentication to authenticate the user but when can use the other authenticate mechanism as well like 
        //Session based application based or any other if there are any
        //Cookie based authentication is recommended as we can dispose them easily
        #endregion
        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            });

        }

    }
}
