using Course.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Course.UI.Controllers
{
    public class StudentController : Controller
    {
        private HttpClient _client;
        public StudentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7124/api/");
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            using (var response = await _client.GetAsync($"students?page={page}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    PaginatedViewModel<StudentViewModelItem> vm = JsonConvert.DeserializeObject<PaginatedViewModel<StudentViewModelItem>>(content);
                    return View(vm);
                }
            }
            return View("Error");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = await _getGroups();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = await _getGroups();
                return View();
            }
            MultipartFormDataContent requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(vm.FullName), "Name");
            requestContent.Add(new StringContent(vm.GroupId.ToString()), "GroupId");
            requestContent.Add(new StringContent(vm.Point.ToString()), "Point");
            using (var response = await _client.PostAsync("students", requestContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Groups = await _getGroups();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var errorVM = JsonConvert.DeserializeObject<ErrorVM>(responseContent);
                    foreach (var item in errorVM.Errors)
                    {
                        ModelState.AddModelError(item.Key, item.ErrorMes);
                    }
                    return View();
                }
            }
            return View("error");
        }
        private async Task<List<GroupViewModelItem>> _getGroups()
        {
            List<GroupViewModelItem> data = new List<GroupViewModelItem>();
            using (var response = await _client.GetAsync("groups/all"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<GroupViewModelItem>>(content);
                }
            }
            return data;
        }


        public async Task<IActionResult> Delete(int id)
        {
            using (var response = await _client.DeleteAsync($"students/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return RedirectToAction("index");
                }
            }
            return View("error");
        }
    }
}

