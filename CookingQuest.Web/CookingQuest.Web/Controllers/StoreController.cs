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
    public class StoreController : Controller
    {
        private readonly MyConfiguration _myConfiguration;
        private readonly string _url;
        private string extensionUrl = "api";

        private readonly HttpClient _httpClient;
        private readonly string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public StoreController(HttpClient httpClient, MyConfiguration myConfiguration)
        {
            _httpClient = httpClient;
            _myConfiguration = myConfiguration;
            _url = _myConfiguration.ServiceUrl + extensionUrl;
        }
        // GET: Store
        public async Task<ActionResult> Index()
        {
            var email = User?.Claims.First(c => c.Type == Email).Value ?? "fortest";

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/player/account/" + email);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            PlayerModel player = await response?.Content.ReadAsAsync<PlayerModel>();

            response = await _httpClient.GetAsync(_url + "/player/equipment/" + player.PlayerId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<EquipmentModel> PlayerEquipment = await response?.Content.ReadAsAsync<IEnumerable<EquipmentModel>>();

            response = await _httpClient.GetAsync(_url + "/equipment/");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<EquipmentModel> equipment = await response?.Content.ReadAsAsync<IEnumerable<EquipmentModel>>();

            response = await _httpClient.GetAsync(_url + "/store/");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            IEnumerable<StoreModel> storeModels = await response?.Content.ReadAsAsync<IEnumerable<StoreModel>>();

            IEnumerable<EquipmentModel> Vouchers = PlayerEquipment.Where(x => x.Type == "Voucher");
            int HighestVoucher = Vouchers.Max(x => x.Difficulty);
            IEnumerable<EquipmentModel> InStock = (equipment.Where(item => !PlayerEquipment.Any(item2 => item2.EquipmentId == item.EquipmentId))).Where(x => x.Type != "Voucher" && x.Difficulty <= HighestVoucher);
            IEnumerable<StoreModel> storeFiltered = storeModels.Where(x => x.Difficulty <= HighestVoucher);
            EquipmentModel NextVoucher = equipment.Where(x => x.Type == "Voucher" && x.Difficulty == HighestVoucher + 1).FirstOrDefault();
            EquipmentModel CurrentVoucher = equipment.Where(x => x.Type == "Voucher" && x.Difficulty == HighestVoucher).FirstOrDefault();

            StoreViewModel storeViewModel = new StoreViewModel
            {
                Player = player,
                InStock = InStock,
                Vouchers = Vouchers,
                PlayerEquipment = PlayerEquipment,
                HighestVoucher = HighestVoucher,
                NextVoucher = NextVoucher,
                CurrentVoucher = CurrentVoucher,
                storeModels = storeFiltered,
            };
            return View(storeViewModel);
        }
                // POST: Player/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(EquipmentModel NextVoucher)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(NextVoucher);
                }
                var email = User?.Claims.First(c => c.Type == Email).Value ?? "fortest";

                HttpResponseMessage response = await _httpClient.GetAsync(_url + "/player/account/" + email);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();

                if (player.Gold - NextVoucher.Price < 0)
                {
                    return View("Error", new ErrorViewModel());
                }
                player.Gold -= NextVoucher.Price;

                response = await _httpClient.PutAsJsonAsync(_url + "/player/" + player.PlayerId, player);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();
                response = await _httpClient.PostAsJsonAsync(_url + "/player/PostEquipment/" + player.PlayerId, NextVoucher);
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }
                var y = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> StoreIndex(int StoreId)
        {
            var email = User?.Claims.First(c => c.Type == Email).Value ?? "fortest";

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/player/account/" + email);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();

            response = await _httpClient.GetAsync(_url + "/store/");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            List<StoreModel> stores = await response.Content.ReadAsAsync<List<StoreModel>>();
            StoreModel storeById = stores.Where(x => x.StoreId == StoreId).FirstOrDefault();

            response = await _httpClient.GetAsync(_url + "/player/loot/" + player.PlayerId.ToString());
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            List<LootModel> loot = await response.Content.ReadAsAsync<List<LootModel>>();

            StoreViewModel storeViewModel = new StoreViewModel
            {
                Player = player,
                Store = storeById,
                Loot = loot,
            };
            return View(storeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> StoreIndex(StoreModel Store, LootModel item, PlayerModel Player)
        {

            var email = User?.Claims.First(c => c.Type == Email).Value ?? "fortest";

            HttpResponseMessage response = await _httpClient.GetAsync(_url + "/player/account/" + email);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            PlayerModel player = await response.Content.ReadAsAsync<PlayerModel>();

            int bonus = Store.Flavors.Where(f => f.FlavorId == item.Flavor.FlavorId).Select(f => f.Bonus).FirstOrDefault();
            player.Gold += item.Price * bonus * item.Quantity;

            response = await _httpClient.PutAsJsonAsync(_url + "/player/" + player.PlayerId, player);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            var x = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();

            response = await _httpClient.DeleteAsync(_url + "/player/DeleteLoot/" + item.PlayerLootId);
            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }
            var y = await response.Content.ReadAsAsync<IEnumerable<ActionResult>>();
            return RedirectToAction(nameof(StoreIndex), new { StoreId = Store.StoreId.ToString() });
        }
        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}