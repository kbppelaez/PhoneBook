using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PhonebookV3.Core;
using PhonebookV3.Core.Application;
using PhonebookV3.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Adds connection to the SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PhonebookDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Dependency Injection for the PhonebookDbContext
builder.Services.AddScoped<PhonebookDbContext>(sp => new PhonebookDbContext());
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddControllersWithViews();

// Adding Cookie Authentication Service
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
//    .AddCookie(options => {
//         options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//         options.SetSlidingExpiration = true;
//         options.AccessDeniedPath = "/Forbidden/";
//    });

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

app.UseAuthorization();

app.MapControllers();

app.Run();
