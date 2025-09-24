using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Infrastructure.DBContext;

namespace UniManagementSystem.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            builder.Services.AddDbContext<UniSystemContext>(options=>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<ApplicationUser,Microsoft.AspNet.Identity.EntityFramework.IdentityRole>()
                .AddEntityFrameworkStores<UniSystemContext>()
                .AddDefaultTokenProviders();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
