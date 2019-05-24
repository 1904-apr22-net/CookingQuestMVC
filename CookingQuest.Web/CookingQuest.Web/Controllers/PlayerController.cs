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
        private readonly string _url = "https://localhost:44336/api/player";
        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public PlayerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            response = await _httpClient.GetAsync(_url + "/locations/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<LocationModel> locations = await response.Content.ReadAsAsync<IEnumerable<LocationModel>>();

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

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Player/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}