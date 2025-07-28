using ePizzaHub.UI.Models.ApiModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ePizzaHub.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("ePizzaAPI");
            var items = await client.GetFromJsonAsync<ApiResponseModel<IEnumerable<ItemResponseModel>>>("Item");
            if (items.Success)
            {
                return View(items.Data);
            }
            return View();
        }
    }
}
