
namespace CatsServer
{
    using CatsServer.Data;
    using CatsServer.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(o =>
            o.UseSqlServer(@"Server=ALEN\SQLEXPRESS01;Database= CatsServerDb;Integrated Security=True;"));
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();
            //app.Use((ctx, next) =>
            //{
            //    app.UseMiddleware<DatabaseMigrationMiddleweare>();
            //    return next();
            //});
                       
            app.UseHtmlContentType();
            //app.Use((ctx, next) =>
            //{
            //    app.UseMiddleware<HtmlContentTypeMiddleweare>();
            //    return next();
            //});

            app.UseStaticFiles();

            app.UseRequestHandler();

            // Order = 1
            //app.MapWhen(ctx => ctx.Request.Path.Value == "/"
            //&& ctx.Request.Method == "GET", home =>
            //{
            //    home.Run(
            //        async (context) =>
            //    {
            //        await context.Response.WriteAsync($"<h1>{env.ApplicationName}</h1>");

            //        var db = context.RequestServices.GetService<CatsDbContext>();

            //        using (db)
            //        {
            //            var catsData = db.Cats
            //                    .Select(c => new { c.Id, c.Name })
            //                    .ToList();

            //            await context.Response.WriteAsync("<ul>");

            //            foreach (var cat in catsData)
            //            {
            //                await context.Response
            //                .WriteAsync($@"<li><a href=""/cat/{cat.Id}"">{cat.Name}</a></li>");
            //            }

            //            await context.Response.WriteAsync("</ul>");
            //            await context.Response.WriteAsync(@"<br/><br/>
            //                    <form action=""/cat/add"">
            //                        <input type=""submit"" value=""AddCat"" />
            //                    </form>");
            //        }
            //    });
            //});

            //Order = 2
            //app.MapWhen(ctx => ctx.Request.Path.Value == "/cat/add", catAdd =>
            //{
            //    catAdd.Run(async context =>
            //    {
            //        if (context.Request.Method == "GET")
            //        {
            //            context.Response.StatusCode = 302;
            //            context.Response.Headers.Add("Location", "/cat-add-form.html");
            //        }
            //        else if (context.Request.Method == "POST")
            //        {
            //            var db = context.RequestServices.GetService<CatsDbContext>();

            //            using (db)
            //            {
            //                var formData = context.Request.Form;

            //                var age = 0;
            //                int.TryParse(formData["Age"], out age);
            //                var cat = new Cat
            //                {
            //                    Name = formData["Name"],
            //                    Age = age,
            //                    Breed = formData["Breed"],
            //                    ImageUrl = formData["ImageUrl"]
            //                };

            //                try
            //                {
            //                    if (string.IsNullOrWhiteSpace(cat.Name) || string.IsNullOrWhiteSpace(cat.Breed) || string.IsNullOrWhiteSpace(cat.ImageUrl))
            //                    {
            //                        throw new InvalidOperationException("invalid cat data.");
            //                    }

            //                    db.Cats.Add(cat);
            //                    await db.SaveChangesAsync();

            //                    context.Response.StatusCode = 302;
            //                    context.Response.Headers.Add("Location", "/");
            //                }
            //                catch
            //                {
            //                    await context.Response.WriteAsync("<h1>invalid cat data</h1>");
            //                    await context.Response.WriteAsync
            //                    (@"<a href=""/cat/add"">Back to the form</a>");
            //                }
            //            }
            //        }
            //    });
            //});

            //Order = 3
            //app.MapWhen(ctx => ctx.Request.Path.Value.StartsWith("/cat") && ctx.Request.Method == "GET",
            //    catDetails =>
            //    {
            //        catDetails.Run(async (context) =>
            //        {
            //            var urlParts = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
            //            if (urlParts.Length < 2)
            //            {
            //                context.Response.StatusCode = 302;
            //                context.Response.Headers.Add("Location", "/");
            //                return;
            //            }

            //            var catId = 0;
            //            int.TryParse(urlParts[1], out catId);
            //            if (catId == 0)
            //            {
            //                context.Response.StatusCode = 302;
            //                context.Response.Headers.Add("Location", "/");
            //                return;
            //            }

            //            var db = context.RequestServices.GetService<CatsDbContext>();

            //            using (db)
            //            {
            //                var cat = await db.Cats.FindAsync(catId);
            //                if (cat == null)
            //                {
            //                    context.Response.StatusCode = 302;
            //                    context.Response.Headers.Add("Location", "/");
            //                    return;
            //                }

            //                await context.Response.WriteAsync($@"<h1>Cat page</h1>
            //                    <br/>
            //                    <h1>{cat.Name}</h1>
            //                    <br/>
            //                    <br/>
            //                    <img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>
            //                    <br/>
            //                    <br/>
            //                    <p>Age: {cat.Age}</p>
            //                    <br />
            //                    <br />
            //                    <p>Breed: {cat.Breed}</p>");
            //            }
            //        });
            //    });


            app.UseNotFoundHandler();
            //app.Run(async (context) =>
            //{
            //    context.Response.StatusCode = 404;
            //    await context.Response.WriteAsync("404 Page was not found :/");
            //});
        }
    }
}
