using URLShortener.Application;
using URLShortener.Domain.Interfaces;
using URLShortener.Infrastructure;
using URLShortener.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddNhibernate(builder.Configuration);
builder.Services.AddMediatRServices();
builder.Services.AddScoped<IUrlRepository, UrlRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
