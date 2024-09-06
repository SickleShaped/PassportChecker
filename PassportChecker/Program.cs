using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PassportChecker.Configurations;
using PassportChecker.Extensions;
using PassportChecker.Services.Implementations;
using System;
using System.Reflection;

namespace PassportChecker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connection = builder.Configuration.GetConnectionString("Default");
        
        builder.Services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo { Title = "PassportChecker", Version = "v1" });
        });

        builder.Services.AddDbContext<ApiDbContext>(options=>options.UseNpgsql(connection));
        builder.Services.AddAutoMapper(typeof(AppMapingProfile));
        builder.Services.AddDependencyInjection(builder.Configuration);
        builder.Services.AddControllersWithViews();
       
        builder.Services.AddHostedService<ReaderHostedService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passports V1"));
        }

        app.UseDBInitialize();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

       
    }
}
