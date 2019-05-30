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
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api";
        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public LocationController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }
        

        // GET: Location
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/Location/");
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
                HttpResponseMessage response2 = await _httpClient.GetAsync(_url + "/Location/Loot/" + l.LocationId);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                IEnumerable<LootModel> loot = await response.Content.ReadAsAsync<IEnumerable<LootModel>>();
                l.Loot = loot;
            }

         

          

            return View(locationsViewModel);
        }



        // GET: Location/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationModel locationModel)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_url + "/Location/", locationModel);
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
        [Authorize(Roles = "Administrator")]
        public ActionResult EditLocation(int LocationId, string Name, string Description, int Difficulty)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLocation(LocationModel locationModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(locationModel);
                }
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync(_url + "/Location/" + locationModel.LocationId, locationModel);
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


                // GET: Location/Delete/5
                public async Task<ActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(_url + "/Location/" + id);
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

        // POST: Location/Delete/5
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