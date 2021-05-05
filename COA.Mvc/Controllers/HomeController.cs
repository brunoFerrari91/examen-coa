using COA.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
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
            try
            {
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.GetAsync("/api/users");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return RedirectToAction("Error", "Home");
                }
                string content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserViewModel>>(content);
                return View(users);
            }
            catch
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            try
            {
                var client = _httpFactory.CreateClient("COA-Api");
                var jsonUser = JsonConvert.SerializeObject(newUser);
                var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/users", stringContent);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
