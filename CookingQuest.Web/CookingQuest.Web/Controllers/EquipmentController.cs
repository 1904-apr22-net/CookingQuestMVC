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
    public class EquipmentController : Controller
    {
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api";

        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public EquipmentController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }
        [AllowAnonymous]
        // GET: Equipment
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/equipment/");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<EquipmentModel> equipment = await response.Content.ReadAsAsync<IEnumerable<EquipmentModel>>();
            return View(equipment);
        }

        // GET: Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EquipmentModel equipmentModel)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url + "/equipment/", equipmentModel);
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

        // GET: Equipment/Edit/5
        public ActionResult Edit(int PlayerEquipmentId, int EquipmentId, string Name, string Type, int Price, int Modifier, int Difficulty)
        {
            return View();
        }

        // POST: Equipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EquipmentModel item)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_url + "/equipment/" + item.EquipmentId, item);
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