
namespace CatsServer.Middlewear
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class HtmlContentTypeMiddleweare
    {
        private readonly RequestDelegate _next;

        public HtmlContentTypeMiddleweare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "text/html");

            return this._next(context);
        }
    }
}
