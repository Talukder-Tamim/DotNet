using Ecommerce.Application.Configurations;
using Ecommerce.Database;
using Ecommerce.Repositories;
using Ecommerce.Repositories.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using WebApplication1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

DependencyConfigurations.Configure(builder.Services);

builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".EcommerceApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.AccessDeniedPath = null;
        options.LogoutPath = null;

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//use, map, run

app.UseException();

app.Use(async (context, next) => 
{
    Debug.WriteLine("Middleware 1 pre processing");
    await next.Invoke();
    Debug.WriteLine("Middleware 1 post processing");
});

app.Use(async (context, next) =>
{
    Debug.WriteLine("Middleware 2 pre processing");
    await next.Invoke();
    Debug.WriteLine("Middleware 2 post processing");
});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 2nd delegate");
//});




app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
