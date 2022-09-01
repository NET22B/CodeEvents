using CodeEvents.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeEvents.Client.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient httpClient;

        public HomeController()
        {
            
           httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7150");
        }

        public IActionResult Index()
        {
            var res = SimpleGet();
            return View();
        }

        private async Task<IEnumerable<object>> SimpleGet()
        {
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}