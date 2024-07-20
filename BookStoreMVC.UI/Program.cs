namespace BookStoreMVC.UI
{
    using BookStoreMVC.Infrastructure.Extentions;
    using BookStoreMVC.UI.Extentions;
    using Microsoft.AspNetCore.Builder;
    using BookStoreMVC.Application.Extensions;
    using Microsoft.Build.Execution;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddUIServices();
            builder.Services.AddApplicationServices();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseAuthentication(); bunu kaldýrmayýnca login ekranýndan ilgili ekrana geçiþ yapmýyor




            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
