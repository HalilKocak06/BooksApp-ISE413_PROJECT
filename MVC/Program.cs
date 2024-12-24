using BLL.DAL;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore; //Bu BLL dosyasýndaki EntityFrameworkCore.SqlServer'dan gelir.
using BLL.Models;
using BLL.Services.Bases;
                                      //Bu sayede Appsettings'i hallederiz.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AppSettings
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings)); //Bunun için Models'de appsetting oluþturduk.
appSettingsSection.Bind(new AppSettings());

//IOC Container:
//string connectionString = builder.Configuration.GetConnectionString("Db"); // Buna ihtiyaç yok çünkü Appsettings'den hallediyoruz artýk.
var connectionString = builder.Configuration.GetConnectionString("Db"); //Bunu kullanmamýz gerekli ...
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IAuthorService, AuthorService>(); // AddSingleton, AddTransient

////Way 1 : 
//builder.Services.AddScoped<IBookService, BookService>();
//builder.Services.AddScoped<IAuthorService, AuthorService>();

//WAY 2 :

builder.Services.AddScoped<IService<Book, BookModel>, BookService>();

builder.Services.AddScoped<IService<Genre, GenreModel>, GenreService>();
builder.Services.AddScoped<IService<User, UserModel>, UserService>();




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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
