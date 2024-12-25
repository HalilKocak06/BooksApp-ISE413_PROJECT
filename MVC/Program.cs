using BLL.DAL;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore; //Bu BLL dosyas�ndaki EntityFrameworkCore.SqlServer'dan gelir.
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
//Bu sayede Appsettings'i hallederiz.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AppSettings
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings)); //Bunun i�in Models'de appsetting olu�turduk.
appSettingsSection.Bind(new AppSettings());

//IOC Container:
//string connectionString = builder.Configuration.GetConnectionString("Db"); // Buna ihtiya� yok ��nk� Appsettings'den hallediyoruz art�k.
var connectionString = builder.Configuration.GetConnectionString("Db"); //Bunu kullanmam�z gerekli ...
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IAuthorService, AuthorService>(); // AddSingleton, AddTransient

////Way 1 : 
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();

//WAY 2 :

builder.Services.AddScoped<IService<Book, BookModel>, BookService>();

builder.Services.AddScoped<IService<Genre, GenreModel>, GenreService>();
builder.Services.AddScoped<IService<User, UserModel>, UserService>();

builder.Services.AddSingleton<HttpServiceBase, HttpService>(); //bu bir singleton'dur ve amac� 1 kez kullan�r ard�ndan kapan�r. 1 ��e i�in.


//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login";
        options.AccessDeniedPath = "/Users/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    }
    );

//Session:

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // default: 20 minutes // 20 dakika sonra session kapan�r.
});



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
//Authentication:

app.UseAuthentication();

app.UseAuthorization();

//Session

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
