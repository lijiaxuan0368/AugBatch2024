using ApplicationCore.ServiceInterface;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Service;
using Infrastructure.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// AddTransient , AddSingleton  
// Single Object
// Http Request
builder.Services.AddDbContext<MovieShopDbContext>(
    option => option.UseSqlServer("Server=LAPTOP-T6D66980\\MSSQLSERVER01;Database=MovieShop;Integrated Security=True;TrustServerCertificate=True;")
);

// builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


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
    pattern: "{controller=Movie}/{action=TopRated}");

app.Run();
