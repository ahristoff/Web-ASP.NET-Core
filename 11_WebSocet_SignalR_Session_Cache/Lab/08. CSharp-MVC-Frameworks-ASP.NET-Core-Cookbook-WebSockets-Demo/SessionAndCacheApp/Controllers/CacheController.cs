
namespace SessionAndCacheApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;

    public class CacheController : Controller
    {
        private readonly IMemoryCache cache;

        public CacheController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string cacheDateKey = "Cache_Current_Date";

            var value = this.cache.Get<string>(cacheDateKey);

            if (value == null)
            {
                value = DateTime.Now.ToString();
                this.cache.Set(cacheDateKey, value, DateTimeOffset.UtcNow.AddMinutes(1));
            }

            return View(model: value);
        }
    }
}
