using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CookingQuest.Web.API_Models;
using CookingQuest.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingQuest.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LootController : Controller
    {
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api";

        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public LootController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }
        // GET: Loot
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/loot/" );
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<LootModel> loot = await response.Content.ReadAsAsync<IEnumerable<LootModel>>();
            return View(loot);
        }

        // GET: Loot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Loot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LootModel lootModel)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url + "/loot/", lootModel);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Loot/Edit/5
        public ActionResult Edit(int PlayerLootId, int LootId, string Name, string Description, int Price)
        {
            return View();
        }

        // POST: Loot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LootModel item)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_url + "/loot/" + item.LootId, item);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}