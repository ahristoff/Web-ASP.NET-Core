
namespace LearningSystem.Web.Infrastructures.Extensions
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScoupe = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScoupe.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();


                var roleManager = serviceScoupe.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScoupe.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var adminName = WebConstants.AdministratorRole; //"Administrator"
                    var blogAuthor = WebConstants.BlogAuthorRole;   //"BlogAuthor"
                    var trainer = WebConstants.TrainerRole;         //"Trainer"

                    var roles = new[]
                    {
                        adminName,
                        blogAuthor,
                        trainer
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            var result = await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }

                    var adminUsername = "admin";
                    var adminEmail = "admin@admin.com";                                                       
                    var adminPassword = "admin123";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail); 

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = adminUsername,
                            Name = adminName,
                            BirthDay = DateTime.Now
                        };
                        await userManager.CreateAsync(adminUser, adminPassword);

                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }

            return app;
        }
    }
}
