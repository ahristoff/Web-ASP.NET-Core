
namespace CatsServer.Middlewear
{
    using CatsServer.Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public class DatabaseMigrationMiddleweare
    {
        private readonly RequestDelegate _next;

        public DatabaseMigrationMiddleweare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.RequestServices.GetRequiredService<CatsDbContext>().Database.Migrate();

            return this._next(context);
        }
    }
}
