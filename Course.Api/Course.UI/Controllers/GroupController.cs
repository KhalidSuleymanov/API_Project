using Course.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Course.UI.Controllers
{
    public class GroupController : Controller
    {
        private HttpClient _clinet;
        public GroupController()
        {
            _clinet = new HttpClient();
            _clinet.BaseAddress = new Uri("https://localhost:7124/api/");
        }
        public async Task<IActionResult> Index(List<int> tagIds, string search)
        {
            ViewBag.TagIds = tagIds;
            ViewBag.Search = search;
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7124/api/groups/all"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        GroupViewModel vm = new GroupViewModel
                        {
                            Groups = JsonConvert.DeserializeObject<List<GroupViewModelItem>>(content)
                        };
                        return View(vm);
                    }
                }
            }
            return View("error");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View();
            using (HttpClient client = new HttpClient())
            {
                StringContent requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync("https://localhost:7124/api/groups", requestContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("index");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorVM = JsonConvert.DeserializeObject<ErrorVM>(responseContent);
                        foreach (var item in errorVM.Errors)
                        {
                            ModelState.AddModelError(item.Key, item.ErrorMes);
                        }
                        return View();
                    }
                }
            }
            return View("Error");
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var response = await _clinet.GetAsync($"groups/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<GroupEditViewModel>(content);
                    return View(vm);
                }
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GroupEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");
            using (var response = await _clinet.PutAsync($"groups/{id}", requestContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
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
    }
}
