using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CookingQuest.Web.API_Models;
using CookingQuest.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CookingQuest.Web.Controllers
{
    public class QuestController : Controller
    {
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api";

        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public QuestController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }

        // GET: Quest
        public async  Task<ActionResult> Index()
        {
            var email = User?.Claims.First(c => c.Type == Email).Value;

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/player/account/" + email);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();

            response = await _httpClient.GetAsync(_url + "/player/equipment/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<EquipmentModel> equipment = await response.Content.ReadAsAsync<IEnumerable<EquipmentModel>>();
            equipment = equipment.Where(x => x.Type != "Voucher" && x.Type != "Cooking");
            IEnumerable<LocationModel> locations;
            response = await _httpClient.GetAsync(_url + "/player/locations/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                locations = new List<LocationModel>();
            }
            else
            {
                locations = await response.Content.ReadAsAsync<IEnumerable<LocationModel>>();
            }

            List<LootModel> loot = new List<LootModel>();
            if (TempData.ContainsKey("loot") && JsonConvert.DeserializeObject<List<LootModel>>(TempData["loot"]. ToString()) is List<LootModel> l)
            {
                loot = l;
            }

            PlayerViewModel viewModel = new PlayerViewModel
            {
                Gold = player.Gold,
                Name = player.Name,
                PlayerId = player.PlayerId,
                Equipment = equipment,
                Locations = locations,
                Loot = loot,
            };
            return View(viewModel);
        }


        // POST: Quest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int LocationId, int Modifier, int PlayerId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_url + "/location/quest/" + LocationId + "/" + Modifier);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                IEnumerable<LootModel> QuestLoot = await response.Content.ReadAsAsync<IEnumerable<LootModel>>();

                response = await _httpClient.PostAsJsonAsync(_url + "/player/NewLootArr/" + PlayerId, QuestLoot);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<ActionResult>();

                TempData["loot"] = JsonConvert.SerializeObject(QuestLoot);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}