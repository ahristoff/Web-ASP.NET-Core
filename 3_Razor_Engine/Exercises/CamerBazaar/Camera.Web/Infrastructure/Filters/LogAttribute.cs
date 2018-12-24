
namespace Camera.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Task.Run(async () =>
            {
                //true -> append log; false -> first delete and then write
                using (var writer = new StreamWriter("logs.txt", true))
                {
                    var dateTime = DateTime.UtcNow;
                    var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                    var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                    var controller = context.Controller.GetType().Name;
                    //var action = context.RouteData.Values["action"];
                    var action = context.ActionDescriptor.RouteValues["action"];

                    var logMessage = $"{dateTime} - {ipAddress} - {userName} - {controller}.{action}";

                    if (context.Exception != null)
                    {
                        var exeptionType = context.Exception.GetType().Name;
                        var exeptionMessage = context.Exception.Message;

                        logMessage = $"[!] {logMessage} - {exeptionType} - {exeptionMessage}";
                    }

                    await writer.WriteLineAsync(logMessage);
                }
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
