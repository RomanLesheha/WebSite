using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSite.Services;
using WebSite.Areas.Identity.Data;
using WebSite.Data;
using WebSite.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductRealization, ProductService>();
builder.Services.AddScoped<IUserRealization, UserServices>();
builder.Services.AddScoped<ICartRepository, CartService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    await DBSeeder.SeedDefaultData(scope.ServiceProvider);
//    DBSeeder.Initialize(scope.ServiceProvider);
//}
//Configure the HTTP request pipeline.
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

app.MapControllerRoute(
    name: "novaPoshta",
    pattern: "nova-poshta/{action}",
    defaults: new { controller = "NovaPoshta", action = "GetAreas" });

app.MapRazorPages();
app.Run();
