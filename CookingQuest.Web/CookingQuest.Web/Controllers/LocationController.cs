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
    public class LocationController : Controller
    {
        private readonly string _url = "https://localhost:44336/api/player";
        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public LocationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Player
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/location/");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<LocationModel> locations = await response.Content.ReadAsAsync<IEnumerable<LocationModel>>();
            IEnumerable<LocationViewModel> locationsViewModel = locations.Select(x => new LocationViewModel()
            {
                LocationId = x.LocationId,
                Difficulty = x.Difficulty,
                Description = x.Description,
                Name = x.Name
            });

            foreach (LocationViewModel l in locationsViewModel)
            {
                HttpResponseMessage response2 = await _httpClient.GetAsync(_url + "/location/loot/" + l.LocationId);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                IEnumerable<LootModel> loot = await response.Content.ReadAsAsync<IEnumerable<LootModel>>();
                l.Loot = loot;
            }

         

          

            return View(locationsViewModel);
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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}