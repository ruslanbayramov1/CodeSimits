using CodeSimits.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CodeSimits
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connStr = builder.Configuration.GetConnectionString("MSSql");
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connStr);
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
