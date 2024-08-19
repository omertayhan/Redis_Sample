using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis_Sample.Models;

namespace Redis_Sample.Controllers
{
    public class HomeController : Controller
    {
        private IDistributedCache _distributedCache; //DI
        public HomeController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<ActionResult> SaveRedisCache() // /Home/SaveRedisCache
        {
            var dashboardData = new DashBoardData
            {
                TopSelllingCountryName = "Turkey",
                TopSelllingProductName = "Laptop",
                TotalCustomerCount = 2310,
                TotalRevenue = 43450
            };

            var tomorrow = DateTime.Now.AddDays(1);
            var totalSecs = tomorrow.Subtract(DateTime.Now).TotalSeconds;

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(totalSecs),
                SlidingExpiration = null,

            };

            var jsonData= JsonConvert.SerializeObject(dashboardData);
            await _distributedCache.SetStringAsync("DashBoardData", jsonData, distributedCacheEntryOptions);

            return View();
        }

        public async Task<ActionResult> Dashboard() // /Home/Dashboard
        {
            var jsonData = await _distributedCache.GetStringAsync("DashBoardData");
            var dashboarData = JsonConvert.DeserializeObject<DashBoardData>(jsonData);
            ViewBag.DashboardData = dashboarData;

            return View();
        }
    }
}
