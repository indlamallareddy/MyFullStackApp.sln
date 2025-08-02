using Microsoft.AspNetCore.Mvc;
using MyFullStackApp.MVC.Models;

namespace MyFullStackApp.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Employees");
            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<List<EmployeeViewModel>>();
                return View(employees);
            }
            else
            {
                // Handle error response
                return View("Error");
                //ModelState.AddModelError(string.Empty, "Unable to load employees.");
            }
            //return View();
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Employees", employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle error response
                    ModelState.AddModelError(string.Empty, "Unable to create employee.");
                }
            }
            return View(employee);

        }
    }
}