using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);
            Console.WriteLine("User Name: " + User.Identity.Name);
            return View();
        }
    }
}
