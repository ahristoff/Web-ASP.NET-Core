
namespace CatsServer.Infrastructure.Handlers
{
    using System;
    using CatsServer.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition =>
            ctx => ctx.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler =>
            async context =>
            {
                if (context.Request.Method == "GET")
                {
                    context.Response.StatusCode = 302;
                    context.Response.Headers.Add("Location", "/cat-add-form.html");
                }
                else if (context.Request.Method == "POST")
                {
                    var db = context.RequestServices.GetRequiredService<CatsDbContext>();

                    using (db)
                    {
                        var formData = context.Request.Form;

                        var age = 0;
                        int.TryParse(formData["Age"], out age);
                        var cat = new Cat
                        {
                            Name = formData["Name"],
                            Age = age,
                            Breed = formData["Breed"],
                            ImageUrl = formData["ImageUrl"]
                        };

                        try
                        {
                            if (string.IsNullOrWhiteSpace(cat.Name) || string.IsNullOrWhiteSpace(cat.Breed) || string.IsNullOrWhiteSpace(cat.ImageUrl))
                            {
                                throw new InvalidOperationException("invalid cat data.");
                            }

                            db.Cats.Add(cat);
                            await db.SaveChangesAsync();

                            context.Response.StatusCode = 302;
                            context.Response.Headers.Add("Location", "/");
                        }
                        catch
                        {
                            await context.Response.WriteAsync("<h1>invalid cat data</h1>");
                            await context.Response.WriteAsync
                            (@"<a href=""/cat/add"">Back to the form</a>");
                        }
                    }
                }
            };
    }
}
