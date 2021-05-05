using COA.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace COA.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _httpFactory;
        public HomeController(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpFactory.CreateClient("COA-Api");
            var response = await client.GetAsync("/api/users");
            if (response.StatusCode != HttpStatusCode.OK)
            {
                RedirectToAction("Error", "Home");
            }
            string content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserViewModel>>(content);
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
