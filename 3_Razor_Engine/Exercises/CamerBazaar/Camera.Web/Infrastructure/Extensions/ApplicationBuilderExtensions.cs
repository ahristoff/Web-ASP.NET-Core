
namespace Camera.Web.Infrastructure.Extensions
{
    using Camera.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScoupe = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScoupe.ServiceProvider.GetService<CameraDbContext>().Database.Migrate();
            }

            return app;
        }
    }
}
