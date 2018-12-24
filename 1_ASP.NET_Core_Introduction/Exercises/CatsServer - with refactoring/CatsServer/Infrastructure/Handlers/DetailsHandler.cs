
namespace CatsServer.Infrastructure.Handlers
{
    using System;
    using CatsServer.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class DetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition =>
            ctx => ctx.Request.Path.Value.StartsWith("/cat") && ctx.Request.Method == "GET";

        public RequestDelegate RequestHandler =>
            async (context) =>
            {
                var urlParts = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (urlParts.Length < 2)
                {
                    context.Response.StatusCode = 302;
                    context.Response.Headers.Add("Location", "/");
                    return;
                }

                var catId = 0;
                int.TryParse(urlParts[1], out catId);
                if (catId == 0)
                {
                    context.Response.StatusCode = 302;
                    context.Response.Headers.Add("Location", "/");
                    return;
                }

                var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                using (db)
                {
                    var cat = await db.Cats.FindAsync(catId);
                    if (cat == null)
                    {
                        context.Response.StatusCode = 302;
                        context.Response.Headers.Add("Location", "/");
                        return;
                    }

                    await context.Response.WriteAsync($@"<h1>Cat page</h1>
                                <br/>
                                <h1>{cat.Name}</h1>
                                <br/>
                                <br/>
                                <img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>
                                <br/>
                                <br/>
                                <p>Age: {cat.Age}</p>
                                <br />
                                <br />
                                <p>Breed: {cat.Breed}</p>");
                }
            };
    }
}
