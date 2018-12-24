namespace RazorPages.Infrastructures.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RazorPages.Data;
    using RazorPages.Data.Models;
    using System.Linq;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScoupe = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScoupe.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

                var userManager = serviceScoupe.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                ApplicationDbContext db = (ApplicationDbContext)serviceScoupe.ServiceProvider.GetService(typeof(ApplicationDbContext));

                if (db.Users.Count() == 0)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        db.Users.Add(new ApplicationUser
                        {
                            Firstname = "Firstname" + i.ToString(),
                            Lastname = "Lastname" + i.ToString(),
                            Email = i.ToString() + "@" + i.ToString(),
                            UserName = "Pesho" + i.ToString(),
                            Age = i
                        });
                    }

                    db.SaveChanges();
                }
            }

            return app;
        }
    }
}