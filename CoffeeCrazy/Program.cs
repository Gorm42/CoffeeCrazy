using CoffeeCrazy.Interfaces;
using CoffeeCrazy.Models;
using CoffeeCrazy.Repos;
using CoffeeCrazy.Services;

namespace CoffeeCrazy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            // repositories
            builder.Services.AddTransient<ICRUDRepo<Job>, JobRepo>();
            builder.Services.AddScoped<ICRUDRepo<Machine>, MachineRepo>();
            builder.Services.AddScoped<ITokenRepo, TokenRepo>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IPasswordRepo, PasswordRepo>();


            // Services
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            builder.Services.AddScoped<IAccessService, AccessService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IJobService, JobService>();

            // Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); 
                options.Cookie.HttpOnly = true; // Makes Cookies Safe
                options.Cookie.IsEssential = true; // Reqires "GDPR-samtykke"
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
         

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Login/Login");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
