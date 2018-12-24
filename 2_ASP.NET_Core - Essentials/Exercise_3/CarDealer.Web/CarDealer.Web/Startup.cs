
namespace CarDealer.Web
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services;
    using CarDealer.Services.Implementations;
    using CarDealer.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarDealerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 4;
                })
                .AddEntityFrameworkStores<CarDealerDbContext>()
                .AddDefaultTokenProviders();

            services.AddDomainServices();
            //services.AddTransient<IEmailSender, EmailSender>();
            //services.AddTransient<ICustomerService, CustomerService>();
            //services.AddTransient<ICarService, CarService>();
            //services.AddTransient<ISupplierService, SupplierService>();
            //services.AddTransient<ISaleService, SaleService>();
            //services.AddTransient<IPartService, PartService>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                // 1 Query
                //routes.MapRoute(
                //       name: "customers",
                //       template: "customers/all/{order}",
                //       defaults: new { controller = "Customers", action = "All" });

                // 2 Query
                //routes.MapRoute(
                //       name: "cars",
                //       template: "cars/{make}",
                //       defaults: new { controller = "Cars", action = "ByMake" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
