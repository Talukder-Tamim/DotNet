using Ecommerce.Database;
using Ecommerce.Repositories;
using Ecommerce.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerRepository>(service =>
{
    var db = service.GetService<ApplicationDbContext>();
    return new CustomerRepository(db);
});

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    string connectionString = "Server=IT-TAMIM; Database=SampleCommerceDB; Trusted_Connection=True; TrustServerCertificate=True;";
    option.UseSqlServer(connectionString);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
