
namespace IdentityDemo.Infrastructure.Extensions
{
    using IdentityDemo.Data;
    using IdentityDemo.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScoupe = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScoupe.ServiceProvider.GetService<IdentityDemoDbContext>().Database.Migrate();



                //----------------------------------assign Role Admin to user--------------
                var roleManager = serviceScoupe.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScoupe.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    // ---------------------create role----------

                    var roleName = "Administrator";

                    var roleExists = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExists)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleName
                        });
                    }
                 // -----------------------------------------------


                 //-------check if such user exists----if not create user and assign the role----

                var adminEmail = "admin@admin.com"; //our Db dont take username only email
                                                    //var adminUsername = "boby";
                var adminPassword = "admin123";

                var adminUser = await userManager.FindByNameAsync(adminEmail); //emal = username

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail  //email = username
                    };
                    await userManager.CreateAsync(adminUser, adminPassword);

                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            })
                .GetAwaiter()
                .GetResult();
            //---------------------------------create Moderator role---------------------

            Task.Run(async () =>
                {
                    var roleName = "Moderator";

                    var roleExists = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExists)
                    {
                        var result = await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = roleName
                        });
                    }
                })
               .GetAwaiter()
               .GetResult();
            }

            return app;
        }
    }
}

