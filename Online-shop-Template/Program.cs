using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_shop_Template.Data.Cart;
using Online_shop_Template.Data.Services;
using Online_shop_Template.Data;
using Online_shop_Template.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IProductServices included from sect 8.
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddScoped<IOrderService, _ordersService>();

builder.Services.AddSingleton<EmailService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Identity and Authorisation
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// added at section 4.13 
var connectionString = builder.Configuration.GetConnectionString("DefaultconnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

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
app.UseSession();

//Configure Authorisation and authentication
app.UseAuthentication();
app.UseAuthorization();

//Change Programme/app.Mapcontroller route/ controller from home to the required action on first launch. Change launchSettings.Json / "launchUrl": "abc" to "" 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

// initialise database of the app.
AppDbInitialiser.Seed(app);
AppDbInitialiser.SeedUsersAndRolesAsync(app).Wait();

app.Run();
