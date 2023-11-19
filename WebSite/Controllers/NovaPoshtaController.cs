using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebSite.Models.NovaPoshta;

namespace WebSite.Controllers
{
    public class NovaPoshtaController : Controller
    {
        private readonly string apiKey = "2b567a196581fab54bc15ce53e5166e5"; // Поміняйте на свій ключ API Нової Пошти
        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAreas()
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            var apiUrl = "https://api.novaposhta.ua/v2.0/json/";
        //            var requestModel = new
        //            {
        //                apiKey,
        //                modelName = "Address",
        //                calledMethod = "getAreas",
        //                methodProperties = new { }
        //            };

        //            var jsonRequest = JsonConvert.SerializeObject(requestModel);
        //            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        //            var response = await client.PostAsync(apiUrl, content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseContent = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<NovaPoshtaResponse>(responseContent);
        //                return View(result.Data);
        //            }
        //            else
        //            {
        //                // Обробити помилку
        //                return View("Error");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обробити помилку
        //        return View("Error");
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> SearchSettlements()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchSettlements(string cityName)
        {

            using (var client = new HttpClient())
            {
                // Формуємо запит до API
                var apiUrl = "https://api.novaposhta.ua/v2.0/json/";
                var requestModel = new
                {
                    apiKey,
                    modelName = "Address",
                    calledMethod = "searchSettlements",
                    methodProperties = new
                    {
                        CityName = cityName,
                        Limit = "50",
                        Page = "1"
                    }
                };

                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NovaPoshtaResponse>(responseContent);
                    if (result.success && result.data != null && result.data[0].Addresses != null)
                    {
                        var settlements = result.data[0].Addresses;
                        return Json(settlements);
                    }
                }
                else
                {
                    // Обробити помилку
                }
            }
            return Json(new List<Settlement>());
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettlements(string cityRef)
        {
            using (var client = new HttpClient())
            {
                // Формуємо запит до API
                string apiUrl = "https://api.novaposhta.ua/v2.0/json/";
                var requestModel = new
                {
                    apiKey,
                    modelName = "Address",
                    calledMethod = "getWarehouses",
                    methodProperties = new
                    {
                        CityRef = cityRef,
                        Page = "1",
                        Limit = "50",
                        Language = "UA",
                    }
                };

                var jsonRequest = JsonConvert.SerializeObject(requestModel);
                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NovaPoshtaResponseWarehouse>(responseContent);
                    if (result.success && result.data != null)
                    {
                        var warehouses = result.data;
                        return Json(warehouses);
                    }
                }
                else
                {

                }
            }

            // Якщо немає даних, то повертаємо пустий список
            var emptyWarehouses = new List<NovaPoshtaDataWarehouse>();
            return Json(emptyWarehouses);
        }



    }
}
