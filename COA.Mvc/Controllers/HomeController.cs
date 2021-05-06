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
            return View(new UserViewModel());
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                //Get user to show in view and check if exists
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.GetAsync($"/api/users/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return RedirectToAction("Error", "Home");
                }
                string content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserViewModel>(content);
                return View("Create", user);
            }
            catch
            {
                throw;
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind]UserViewModel newUser)
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
                HttpResponseMessage response;
                if (newUser.IdUsuario == 0) // Default value for new user
                {
                    response = await client.PostAsync("/api/users", stringContent);
                }
                else // User already exists
                {
                    response = await client.PutAsync($"/api/users/{newUser.IdUsuario}", stringContent);
                }
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

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //Get user to show in view and check if exists
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.GetAsync($"/api/users/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return RedirectToAction("Error", "Home");
                }
                string content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserViewModel>(content);
                return View(user);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.DeleteAsync($"/api/users/{id}");
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
