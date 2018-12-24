
namespace Camera.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessureTimeAttribute: ActionFilterAttribute
    {
        private Stopwatch stopWatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.stopWatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.stopWatch.Stop();
            using (var writer = new StreamWriter(@"Infrastructure\Filters\Logs\action-times.txt", true))
            {
                var dateTime = DateTime.UtcNow;
                var controler = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];
                var elapsedTime = this.stopWatch.Elapsed;

                var logMessage = $"{dateTime} - {controler}.{action} - {elapsedTime}";

                writer.WriteLine(logMessage);
            }
        }
    }
}
