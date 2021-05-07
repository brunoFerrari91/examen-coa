using COA.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace COA.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;
        public HomeController(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.GetAsync("/api/users");
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<ErrorViewModel>(content);
                    var errorMessage = error.ErrorList["error"][0];
                    return View("error", errorMessage);
                }
                var users = JsonConvert.DeserializeObject<List<UserViewModel>>(content);
                int pageNumber = (page ?? 1);                
                return View(users.ToPagedList(pageNumber,10));
            }
            catch
            {
                return View("error");
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
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<ErrorViewModel>(content);
                    var errorMessage = error.ErrorList["error"][0];
                    return View("error", errorMessage);
                }
                var user = JsonConvert.DeserializeObject<UserViewModel>(content);
                return View("Create", user);
            }
            catch
            {
                return View("error");
            }            
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
                HttpResponseMessage response;
                if (newUser.IdUsuario == 0) // Default value for new user
                {
                    response = await client.PostAsync("/api/users", stringContent);
                }
                else // User already exists
                {
                    response = await client.PutAsync($"/api/users/{newUser.IdUsuario}", stringContent);
                }
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<ErrorViewModel>(content);
                    var errorMessage = error.ErrorList["error"][0];
                    return View("error", errorMessage);
                }
                TempData["success"] = "true";
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("error");
            }
        }
                
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpFactory.CreateClient("COA-Api");
                var response = await client.DeleteAsync($"/api/users/{id}");
                string content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var error = JsonConvert.DeserializeObject<ErrorViewModel>(content);
                    var errorMessage = error.ErrorList["error"][0];
                    return View("error", errorMessage);
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
