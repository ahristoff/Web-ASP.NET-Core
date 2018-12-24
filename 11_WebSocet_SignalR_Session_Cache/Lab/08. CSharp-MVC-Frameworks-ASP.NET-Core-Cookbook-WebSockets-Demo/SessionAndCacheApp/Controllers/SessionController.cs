
namespace SessionAndCacheApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Http;

    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            const string sessionDateKey = "Session_Current_Date";

            var value = this.HttpContext.Session.GetString(sessionDateKey);

            if (value == null)
            {
                value = DateTime.Now.ToString();
                this.HttpContext.Session.SetString(sessionDateKey, value);
            }

            return View(model: value);
        }
    }
}
