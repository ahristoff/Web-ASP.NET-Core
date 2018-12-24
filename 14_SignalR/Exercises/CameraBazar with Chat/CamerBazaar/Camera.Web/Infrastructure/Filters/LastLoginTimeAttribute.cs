
namespace Camera.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LastLoginTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter(@"Infrastructure\Filters\Logs\lastLogin.txt"))
            {
                var dateTime = DateTime.UtcNow;

                writer.WriteLine(dateTime);
            }
        }
    }
}
