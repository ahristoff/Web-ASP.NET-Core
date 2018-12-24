
namespace CatsServer.Infrastructure.Extensions
{
    using CatsServer.Infrastructure.Handlers;
    using CatsServer.Middlewear;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Reflection;

    public static class ApplicationBuilderExtentions
    {
        // in StartUp app.UseDatabaseMigration()
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleweare>();

        // in StartUp app.UseHtmlContentType()
        public static IApplicationBuilder UseHtmlContentType(this IApplicationBuilder builder)
           => builder.UseMiddleware<HtmlContentTypeMiddleweare>();

        // in StartUp app.UseRequestHandler();
        public static IApplicationBuilder UseRequestHandler(this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(System.Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Condition, app =>
                {
                    app.Run(handler.RequestHandler);
                });
            }

            return builder;
        }

        //in StartUp app.UseNotFoundHandler();
        public static void UseNotFoundHandler(this IApplicationBuilder builder)
        {
            builder.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page was not found :/");
            });
        }
    }
}
