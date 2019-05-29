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
    [Authorize]
    public class PlayerController : Controller
    {
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api/player";

        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public PlayerController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }


        // GET: Player
        public async Task<ActionResult> Index()
        {
            var email = User.Claims.First(c => c.Type == Email).Value;

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/account/" + email);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();

            response = await _httpClient.GetAsync(_url + "/equipment/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<EquipmentModel> equipment = await response.Content.ReadAsAsync<IEnumerable<EquipmentModel>>();

            response = await _httpClient.GetAsync(_url + "/loot/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<LootModel> loot = await response.Content.ReadAsAsync<IEnumerable<LootModel>>();

            IEnumerable<LocationModel> locations;
            response = await _httpClient.GetAsync(_url + "/locations/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                locations = new List<LocationModel>();
            }
            else
            {
                locations = await response.Content.ReadAsAsync<IEnumerable<LocationModel>>();
            }

            PlayerViewModel viewModel = new PlayerViewModel
            {
                Gold = player.Gold,
                Name = player.Name,
                PlayerId = player.PlayerId,
                Equipment = equipment,
                Loot = loot,
                Locations = locations
            };

            return View(viewModel);
        }

        // GET: Player/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int PlayerLootId, int LootId, string Name, string Description, int Price, int Quantity)
        {
            return View();
        }

        // POST: Player/Edit
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LootModel lootModel )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(lootModel);
                }
                var email = User.Claims.First(c => c.Type == Email).Value;

                HttpResponseMessage response = await _httpClient.GetAsync(_url + "/account/" + email);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();


                response = await _httpClient.PostAsJsonAsync(_url + "/Loot/" + player.PlayerId, lootModel);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Player/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult EditPlayer(int PlayerId, string Name, int Gold)
        {
            return View();
        }
        // POST: Player/Edit
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPlayer(PlayerModel playerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(playerModel);
                }
                var email = User.Claims.First(c => c.Type == Email).Value;

                HttpResponseMessage response = await _httpClient.GetAsync(_url + "/account/" + email);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();


                response = await _httpClient.PutAsJsonAsync(_url + "/" + player.PlayerId, playerModel);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Player/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult EditEquipment(int PlayerEquipmentId, int EquipmentId, string Name, string Type, int Price, int Modifier, int Difficulty)
        {
            return View();
        }
        // POST: Player/Edit
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEquipment(EquipmentModel equipmentModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(equipmentModel);
                }
                var email = User.Claims.First(c => c.Type == Email).Value;

                HttpResponseMessage response = await _httpClient.GetAsync(_url + "/account/" + email);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();


                response = await _httpClient.PostAsJsonAsync(_url + "/Equipment/" + player.PlayerId, equipmentModel);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Player/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Player/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LootModel lootModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(lootModel);
                }
                var email = User.Claims.First(c => c.Type == Email).Value;

                HttpResponseMessage response = await _httpClient.DeleteAsync(_url + "/DeleteLoot/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: Player/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEquipment(int id, EquipmentModel equipmentModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(equipmentModel);
                }
                var email = User.Claims.First(c => c.Type == Email).Value;

                HttpResponseMessage response = await _httpClient.DeleteAsync(_url + "/DeleteEquipment/" + id);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}